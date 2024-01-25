/// <summary>
/// MainForm is the main form and entry point for the application.
/// It handles initializing the UI components, setting up the timer,
/// tracking active application changes, calculating time spent per app,
/// and updating the UI.
/// </summary>
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppTimeTracker
{
    public partial class MainForm : Form
    {
        private ApplicationTracker _tracker = new ApplicationTracker();
        private ApplicationListView _appListView;
        private Dictionary<string, ApplicationData> _appData = new Dictionary<string, ApplicationData>();
        private string _currentApp = "";
        private DateTime _lastSwitchTime;
        private System.Windows.Forms.Timer _timer;
        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _statusLabel;
        private ListView _listView; // ListView as a field

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetUpTimer();

            _appListView = new ApplicationListView(_listView);
            _lastSwitchTime = DateTime.Now;
        }

        private void InitializeCustomComponents()
        {
            // ListView Setup
            _listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };
            _listView.Columns.Add("Application", 200, HorizontalAlignment.Left);
            _listView.Columns.Add("Time Spent", 150, HorizontalAlignment.Left);
            _listView.ColumnWidthChanging += ListView_ColumnWidthChanging;
            Controls.Add(_listView);

            // Status Bar Setup
            _statusStrip = new StatusStrip();
            _statusLabel = new ToolStripStatusLabel { Text = "Ready" };
            _statusStrip.Items.Add(_statusLabel);
            Controls.Add(_statusStrip);

            // Refresh Button Setup
            Button refreshButton = new Button
            {
                Text = "Refresh",
                Dock = DockStyle.Bottom
            };
            refreshButton.Click += RefreshButton_Click;
            Controls.Add(refreshButton);
        }

        private void SetUpTimer()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;
            _timer.Start();
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

            _appListView.Update(_appData.Values.ToList());
        }

        private void RefreshButton_Click(object? sender, EventArgs e)
        {
            _appListView.Update(_appData.Values.ToList());
            _statusLabel.Text = "List refreshed at " + DateTime.Now.ToString("T");
        }

        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true; // Prevent column resize
        }
    }
}
