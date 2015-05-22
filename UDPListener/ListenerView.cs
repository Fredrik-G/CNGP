using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogAnalysis;

namespace UDPListener
{
    public partial class ListenerView : UserControl
    {
        #region Data

        private readonly List<LogRowInfo> _logRowInfos = new List<LogRowInfo>();

        private UdpClient _udpServer;
        public bool UdpListenIsActive;

        /// <summary>
        /// Events used to signal starting/stopping a listener.
        /// </summary>
        public event EventHandler StartedNewListener;

        public event EventHandler StoppedListener;
        public event EventHandler TryingToStartNewListener;

        private readonly ActiveTime _activeTime = new ActiveTime();

        #endregion

        #region Properties

        public int Port
        {
            get { return Convert.ToInt16(PortTextBox.Text); }
            set { PortTextBox.Text = value.ToString(); }
        }

        #endregion

        #region Constructor & Load

        /// <summary>
        /// Default constructor
        /// </summary>
        public ListenerView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when this UserControl is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerView_Load(object sender, EventArgs e)
        {
            LogLevelComboBox.SelectedIndex = 0;
            TimeActiveLabel.Text = String.Empty;
        }

        #endregion

        #region Button Clicks

        /// <summary>
        /// Occurs when the Start Listener Button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartListenerButton_Click(object sender, EventArgs e)
        {
            PreparingToStartNewListener();
        }

        /// <summary>
        /// Occurs when the Start Listener Button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopListenerButton_Click(object sender, EventArgs e)
        {
            StopListening();
        }

        /// <summary>
        /// Occurs when the Clear Messages Button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearMessagesButton_Click(object sender, EventArgs e)
        {
            ClearAllMessages();
        }

        #endregion

        #region Textbox Mouse Clicks

        /// <summary>
        /// Occurs when the IPAdressTextBox is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IPAdressTextBox_MouseDown(object sender, MouseEventArgs e)
        {
           // IPAdressTextBox.Text = String.Empty;
        }

        /// <summary>
        /// Occurs when the PortTextBox is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            PortTextBox.Text = String.Empty;
        }

        #endregion

        #region UDP Listener Methods

        /// <summary>
        /// Prepares to start a new listener
        /// Raises the "TryingToStartNewListener"-event.
        /// </summary>
        private void PreparingToStartNewListener()
        {
            if (TryingToStartNewListener != null)
                TryingToStartNewListener(this, new EventArgs());
        }

        /// <summary>
        /// Starts up the server and starts listening for incoming messages.
        /// </summary>
        public void StartListening()
        {
            if (UdpListenIsActive)
                return;

            _udpServer = new UdpClient(Port);
            UdpListenIsActive = true;
            ReceiveUdpMessage();

            //IPAdressTextBox.Enabled = PortTextBox.Enabled = false;
            StartListenerButton.Enabled = false;
            StopListenerButton.Enabled = true;
            TimeActiveLabel.Visible = true;

            TimeActiveTimer.Start();

            if (StartedNewListener != null)
                StartedNewListener(this, new EventArgs());
        }

        /// <summary>
        /// Stops listening on the server.
        /// </summary>
        public void StopListening()
        {
            if (UdpListenIsActive)
            {
                UdpListenIsActive = false;
                _udpServer.Client.Close();

                //IPAdressTextBox.Enabled = PortTextBox.Enabled = true;
                StartListenerButton.Enabled = true;
                StopListenerButton.Enabled = false;

                TimeActiveTimer.Stop();

                if (StoppedListener != null)
                    StoppedListener(this, new EventArgs());
            }
        }

        /// <summary>
        /// Receives an UDP message asynchronously.
        /// </summary>
        private void ReceiveUdpMessage()
        {
            _udpServer.BeginReceive(ReceiveUdpCallback, null);
        }

        /// <summary>
        /// Ends an asynchronous UDP message.
        /// Also calls for the next message if the listener is active.
        /// </summary>
        /// <param name="result"></param>
        private void ReceiveUdpCallback(IAsyncResult result)
        {
            if (UdpListenIsActive)
            {
                var ipAdress = new IPEndPoint(IPAddress.Any, Port);
                var receivedResults = _udpServer.EndReceive(result, ref ipAdress);
                var loggingEvent = Encoding.ASCII.GetString(receivedResults);

                var logRowInfo = new LogRowInfo();
                logRowInfo.FormatLine(loggingEvent);

                AddNewLogMessage(logRowInfo);

                if (UdpListenIsActive)
                {
                    ReceiveUdpMessage();
                }
            }
        }

        #endregion

        /// <summary>
        /// Adds a new log message to the list and datagridview.
        /// </summary>
        /// <param name="logRowInfo"></param>
        private void AddNewLogMessage(LogRowInfo logRowInfo)
        {
            Invoke(new Action(() => { _logRowInfos.Add(logRowInfo); }));
            Invoke(new Action(() =>
            {
                LogMessagesDataGridView.Rows.Add(logRowInfo.DateTime, logRowInfo.Level, logRowInfo.Thread,
                    logRowInfo.Logger, logRowInfo.Message);
            }));

            var indexOfLastAddedRow = LogMessagesDataGridView.Rows.GetLastRow(DataGridViewElementStates.None);
            ColorRowAfterItsLevel(indexOfLastAddedRow, logRowInfo);
            FilterLogMessages();

            _activeTime.AddMessage();
        }

        /// <summary>
        /// Clears all log messages.
        /// </summary>
        private void ClearAllMessages()
        {
            _logRowInfos.Clear();
            LogMessagesDataGridView.Rows.Clear();
            _activeTime.ClearMessageCount();
            TimeActiveLabel.Text = String.Empty;
        }

        /// <summary>
        /// Colors the last inserted row in the datagridview based on given log level.
        /// </summary>
        /// <param name="indexOfLastAddedRow"></param>
        /// <param name="logRowInfo"></param>
        private void ColorRowAfterItsLevel(int indexOfLastAddedRow, LogRowInfo logRowInfo)
        {
            Color selectedColor;
            switch (logRowInfo.Level)
            {
                case "DEBUG":
                    selectedColor = Color.PeachPuff;
                    break;
                case "INFO ":
                    selectedColor = Color.PowderBlue;
                    break;
                case "WARN ":
                    selectedColor = Color.Orange;
                    break;
                case "ERROR":
                    selectedColor = Color.MediumVioletRed;
                    break;
                case "FATAL":
                    selectedColor = Color.DarkRed;
                    break;
                default:
                    selectedColor = Color.Black;
                    break;
            }
            LogMessagesDataGridView.Rows[indexOfLastAddedRow].DefaultCellStyle.BackColor = selectedColor;
        }

        /// <summary>
        /// Filters the log messages based on the selected filter value in the combo box.
        /// </summary>
        private void FilterLogMessages()
        {
            var logLevel = 0;
            Invoke(new Action(() => { logLevel = LogLevelComboBox.SelectedIndex; }));

            foreach (DataGridViewRow logMessageRow in LogMessagesDataGridView.Rows)
            {
                switch (logLevel)
                {
                    case 0:
                        Invoke(new Action(() => { logMessageRow.Visible = true; }));
                        break;
                    case 1:
                        Invoke(
                            new Action(
                                () => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("FATAL"); }));
                        break;
                    case 2:
                        Invoke(
                            new Action(
                                () => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("ERROR"); }));
                        break;
                    case 3:
                        Invoke(
                            new Action(
                                () => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("WARN "); }));
                        break;
                    case 4:
                        Invoke(
                            new Action(
                                () => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("INFO "); }));
                        break;
                    case 5:
                        Invoke(
                            new Action(
                                () => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("DEBUG"); }));
                        break;
                }
            }
        }

        #region Combobox Events

        private void LogLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterLogMessages();
        }

        #endregion

        #region Timer Events

        private void TimeActiveTimer_Tick(object sender, EventArgs e)
        {
            if (TimeActiveTimer.Interval < 1000)
            {
                throw new Exception("Time Interval should not be set to lower than 1000ms");
            }

            for (var i = 0; i < TimeActiveTimer.Interval/1000; i++)
            {
                _activeTime.AddSecond();
            }

            TimeActiveLabel.Text = _activeTime.GetActiveTimeString();

            TimeActiveTimer.Start();
        }

        #endregion

        private void SaveLogsButton_Click(object sender, EventArgs e)
        {
            SaveLogMessages();
        }

        /// <summary>
        /// Writes each log message to user selected file.
        /// </summary>
        private void SaveLogMessages()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Log file|*.log";
            saveFileDialog.Title = "Save Log Messages";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var logRowInfo in _logRowInfos)
                    {
                        fileStream.Write(logRowInfo.CreateLogString());
                    }
                }
            }
        }
    }
}
