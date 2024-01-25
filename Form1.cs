using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AppTimeTracker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private Dictionary<string, TimeSpan> _appUsage = new Dictionary<string, TimeSpan>();
        private string _currentApp = "";
        private DateTime _lastSwitchTime;
        private System.Windows.Forms.Timer _timer;

        private ListBox listBoxAppUsage;
        private ListView listViewAppUsage;
        private ImageList imageListIcons;

        private TimeSpan GetIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            GetLastInputInfo(ref lastInputInfo);

            uint lastInputTick = lastInputInfo.dwTime;
            uint currentTick = (uint)Environment.TickCount;

            return TimeSpan.FromMilliseconds(currentTick - lastInputTick);
        }

        public Form1()
        {
            InitializeComponent();

            // Initialize the ListView and add it to the form
            listViewAppUsage = new ListView();
            listViewAppUsage.Dock = DockStyle.Fill;
            listViewAppUsage.View = View.Details;
            listViewAppUsage.FullRowSelect = true;
            listViewAppUsage.Columns.Add("Application", -2, HorizontalAlignment.Left);
            listViewAppUsage.Columns.Add("Time Spent", -2, HorizontalAlignment.Left);
            this.Controls.Add(listViewAppUsage);

            // Initialize the ImageList and assign it to the ListView
            imageListIcons = new ImageList();
            listViewAppUsage.SmallImageList = imageListIcons;

            // Initialize the ListBox and add it to the form
            listBoxAppUsage = new ListBox();
            listBoxAppUsage.Dock = DockStyle.Fill;
            this.Controls.Add(listBoxAppUsage);

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // Check every second
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _lastSwitchTime = DateTime.Now;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan idleTime = GetIdleTime();
            const int idleThreshold = 5 * 60 * 1000; // 5 minutes in milliseconds

            if (idleTime.TotalMilliseconds > idleThreshold)
            {
                // If the user has been idle for more than 5 minutes, consider this as idle time
                // and do not update the current app's time.
                _lastSwitchTime = DateTime.Now;
            }
            else
            {
                string activeApp = GetActiveWindowTitle();

                // Update the time for the current app
                if (!string.IsNullOrEmpty(_currentApp))
                {
                    if (!_appUsage.ContainsKey(_currentApp))
                    {
                        _appUsage[_currentApp] = TimeSpan.Zero;
                    }
                    _appUsage[_currentApp] += DateTime.Now - _lastSwitchTime;
                }

                _lastSwitchTime = DateTime.Now;

                // If the active window has changed, update the current app
                if (_currentApp != activeApp)
                {
                    _currentApp = activeApp;
                }

                UpdateAppUsageList();
            }
        }

        private void UpdateAppUsageList()
        {
            // Sort the app usage dictionary by time spent
            var sortedAppUsage = new List<KeyValuePair<string, TimeSpan>>(_appUsage);
            sortedAppUsage.Sort((firstPair, nextPair) => nextPair.Value.CompareTo(firstPair.Value));

            listViewAppUsage.BeginUpdate();
            listViewAppUsage.Items.Clear(); // Clear the current list

            foreach (var app in _appUsage)
            {
                string appName = Path.GetFileNameWithoutExtension(app.Key);
                string appTime = app.Value.ToString(@"hh\:mm\:ss");

                // Attempt to load the icon for the application
                try
                {
                    if (!imageListIcons.Images.ContainsKey(appName)) // Check if the icon is already loaded
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(app.Key);
                        imageListIcons.Images.Add(appName, appIcon);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error extracting icon for {app.Key}: {ex.Message}");
                    // Use a default icon if the specific icon cannot be loaded
                    //if (!imageListIcons.Images.ContainsKey("DefaultIcon"))
                    //{
                    //    // Assuming you named the default icon "DefaultIcon" in your resources
                    //    Icon defaultIcon = Properties.Resources.DefaultIcon;
                    //    imageListIcons.Images.Add("DefaultIcon", defaultIcon);
                    //}
                    //appName = "DefaultIcon"; // Use the default icon key
                }

                ListViewItem item = new ListViewItem(appName);
                item.SubItems.Add(appTime);
                item.ImageKey = appName;
                listViewAppUsage.Items.Add(item);
            }

            listViewAppUsage.EndUpdate();
        }

        private string GetActiveWindowTitle()
        {
            IntPtr handle = GetForegroundWindow();
            if (handle == IntPtr.Zero) return "";

            uint processId;
            GetWindowThreadProcessId(handle, out processId);

            Process foregroundProcess = Process.GetProcessById((int)processId);
            return Path.GetFileNameWithoutExtension(foregroundProcess.MainModule.FileName);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _timer.Stop();

            if (!string.IsNullOrEmpty(_currentApp))
            {
                if (!_appUsage.ContainsKey(_currentApp))
                {
                    _appUsage[_currentApp] = TimeSpan.Zero;
                }
                _appUsage[_currentApp] += DateTime.Now - _lastSwitchTime;
            }

            foreach (var app in _appUsage)
            {
                Debug.WriteLine($"App: {app.Key}, Time Spent: {app.Value}");
            }
        }
    }
}