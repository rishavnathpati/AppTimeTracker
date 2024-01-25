using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AppTimeTracker
{
    public partial class MainForm : Form
    {
        private ApplicationTracker _tracker = new ApplicationTracker();
        private ApplicationListView _listView;
        private Dictionary<string, ApplicationData> _appData = new Dictionary<string, ApplicationData>();
        private string _currentApp = "";
        private DateTime _lastSwitchTime;
        private System.Windows.Forms.Timer _timer;

        public MainForm()
        {
            InitializeComponent();

            ListView listView = new ListView();
            listView.Dock = DockStyle.Fill;
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.Columns.Add("Application", -2, HorizontalAlignment.Left);
            listView.Columns.Add("Time Spent", -2, HorizontalAlignment.Left);
            this.Controls.Add(listView);

            _listView = new ApplicationListView(listView);

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // Check every second
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _lastSwitchTime = DateTime.Now;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            string activeApp = _tracker.GetActiveWindowTitle();

            if (!string.IsNullOrEmpty(_currentApp))
            {
                if (!_appData.ContainsKey(_currentApp))
                {
                    _appData[_currentApp] = new ApplicationData(_currentApp) { TimeSpent = TimeSpan.Zero };
                }
                _appData[_currentApp].TimeSpent += DateTime.Now - _lastSwitchTime;
            }

            _lastSwitchTime = DateTime.Now;

            if (_currentApp != activeApp)
            {
                _currentApp = activeApp;
            }

            _listView.Update(_appData.Values.ToList());
        }
    }
}