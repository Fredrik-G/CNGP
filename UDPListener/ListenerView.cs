using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

        public event EventHandler StartedNewListener;
        public event EventHandler StoppedListener;

        #endregion

        #region Properties

        public string IpAdress
        {
            get { return IPAdressTextBox.Text; }
            set { IPAdressTextBox.Text = value; }
        }

        public int Port
        {
            get { return Convert.ToInt16(PortTextBox.Text); }
            set { PortTextBox.Text = value.ToString(); }
        }

        #endregion

        #region Constructor & Load

        public ListenerView()
        {
            InitializeComponent();
        }

        private void ListenerView_Load(object sender, EventArgs e)
        {
            LogLevelComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Button Clicks

        private void StartListenerButton_Click(object sender, EventArgs e)
        {
            StartListening();

        }

        private void StopListenerButton_Click(object sender, EventArgs e)
        {
            StopListening();
        }

        #endregion

        #region Textbox Mouse Clicks

        private void IPAdressTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            IPAdressTextBox.Text = String.Empty;
        }

        private void PortTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            PortTextBox.Text = String.Empty;
        }

        #endregion

        #region UDP Listener Methods

        /// <summary>
        /// Starts up the server and starts listening for incoming messages.
        /// </summary>
        public void StartListening()
        {
            if (UdpListenIsActive)
                return;

            IPAdressTextBox.Enabled = PortTextBox.Enabled = false;
            StartListenerButton.Enabled = false;
            StopListenerButton.Enabled = true;

            _udpServer = new UdpClient(Port);
            UdpListenIsActive = true;
            var task = Task.Run(() => ReceiveUdpMessage());

            if(StartedNewListener != null)
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

                IPAdressTextBox.Enabled = PortTextBox.Enabled = true;
                StartListenerButton.Enabled = true;
                StopListenerButton.Enabled = false;

                if (StoppedListener != null)
                    StoppedListener(this, new EventArgs());
            }
        }

        private void ReceiveUdpMessage()
        {
            _udpServer.BeginReceive(ReceiveUdpCallback, null);
        }

        private void ReceiveUdpCallback(IAsyncResult result)
        {
            if (UdpListenIsActive)
            {
                var ipAdress = new IPEndPoint(IPAddress.Parse(IpAdress), Port);
                var receivedResults = _udpServer.EndReceive(result, ref ipAdress);
                var loggingEvent = Encoding.ASCII.GetString(receivedResults);

                var logRowInfo = new LogRowInfo();
                logRowInfo.FormatLine(loggingEvent);

                // Invoke(new Action(() => { _logRowInfos.Add(logRowInfo); }));

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
                LogMessagesDataGridView.Rows.Add(logRowInfo.DateTime, logRowInfo.Level, logRowInfo.Thread, logRowInfo.Logger, logRowInfo.Message);
            }));

            var indexOfLastAddedRow = LogMessagesDataGridView.Rows.GetLastRow(DataGridViewElementStates.None);
            ColorRowAfterItsLevel(indexOfLastAddedRow, logRowInfo);
            FilterLogMessages();
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

        private void LogLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterLogMessages();
        }

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
                        Invoke(new Action(() => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("FATAL"); }));
                        break;
                    case 2:
                        Invoke(new Action(() => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("ERROR"); }));
                        break;
                    case 3:
                        Invoke(new Action(() => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("WARN "); }));
                        break;
                    case 4:
                        Invoke(new Action(() => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("INFO "); }));
                        break;
                    case 5:
                        Invoke(new Action(() => { logMessageRow.Visible = logMessageRow.Cells["Level"].Value.Equals("DEBUG"); }));
                        break;
                }
            }
        }
    }
}
