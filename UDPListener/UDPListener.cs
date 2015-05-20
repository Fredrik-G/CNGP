using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogAnalysis;

namespace UDPListener
{
    public partial class UDPListener : Form
    {
        #region Data

        private List<ListenerView> _listenerViews = new List<ListenerView>();

        #endregion

        #region Constructor & Load

        //Default constructor
        public UDPListener()
        {
            InitializeComponent();
        }

        private void UDPListener_Load(object sender, EventArgs e)
        {
            StartNewListener();
        }

        #endregion

        private void ListenerView_StartedNewListener(object sender, EventArgs e)
        {
            UpdateInfoFormTitle();
        }

        private void ListenerView_StoppedListener(object sender, EventArgs e)
        {
            UpdateInfoFormTitle();
        }

        private void UpdateInfoFormTitle()
        {
            var numberOfActiveListeners = _listenerViews.Count(listenerView => listenerView.UdpListenIsActive);
            var totalNumberOfListeners = _listenerViews.Count;
            Text = totalNumberOfListeners + " Listener(s) Up   -    " + numberOfActiveListeners + "/" + totalNumberOfListeners + " Active Listeners";
        }

        #region Listener Methods

        private void StartNewListener()
        {
            var listenerView = new ListenerView
            {
                Size = Size,
                Dock = DockStyle.Fill
                //  Anchor = AnchorStyles.Bottom | AnchorStyles.Right |AnchorStyles.Top | AnchorStyles.Left
            };
            var numerOfTabs = ListenerTabControl.TabPages.Count + 1;
            var tabPage = new TabPage {Text = "Listener " + numerOfTabs};
            tabPage.Controls.Add(listenerView);
            ListenerTabControl.Controls.Add(tabPage);
            _listenerViews.Add(listenerView);

            AddNewListenerEventToLastListenerView();
            UpdateInfoFormTitle();
        }

        /// <summary>
        /// Binds the "StartedNewListener"-event to the function ListenerView_StartedNewListener
        /// for the last inserted element in the ListenerView-list.
        /// </summary>
        private void AddNewListenerEventToLastListenerView()
        {
            _listenerViews[_listenerViews.Count - 1].StartedNewListener += ListenerView_StartedNewListener;
            _listenerViews[_listenerViews.Count - 1].StoppedListener += ListenerView_StoppedListener;
        }

        private void StopAllListeners()
        {
            foreach (var listenerView in _listenerViews)
            {
                listenerView.StopListening();
            }
        }

        #endregion

        #region ToolStipMenu Clicks

        private void startNewListenerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            StartNewListener();
        }

        private void stopAllListenersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAllListeners();
        }

        #endregion
    }
}
//#region UDP Listener Methods

///// <summary>
///// UDPClient that listen for incoming UDP packets. 
///// Used to listen for log messages.
///// </summary>
///// <param name="cancellationToken"></param>
//private async void UDPLogListener(CancellationToken cancellationToken)
//{
//    using (var udpClient = new UdpClient(9059))
//        while (true)
//        {
//            // Was cancellation already requested?  
//            if (cancellationToken.IsCancellationRequested)
//            {
//                cancellationToken.ThrowIfCancellationRequested();
//            }

//            var receivedResults = await udpClient.ReceiveAsync();
//            var loggingEvent = Encoding.ASCII.GetString(receivedResults.Buffer);

//            var logRowInfo = new LogRowInfo();
//            logRowInfo.FormatLine(loggingEvent);

//            _logRowInfos.Add(logRowInfo);

//            if (cancellationToken.IsCancellationRequested)
//            {
//                cancellationToken.ThrowIfCancellationRequested();
//            }
//        }
//}

///// <summary>
///// Starts a new UDP listener task.
///// </summary>
//private void StartUDPListenerTask()
//{
//    _cancellationToken = _cancellationTokenSource.Token;
//    _udpListenerTask = Task.Run(async () => UDPLogListener(_cancellationToken), _cancellationToken);
//}

///// <summary>
///// Stops the UDP listener if it has been started.
///// </summary>
//private void StopUDPListenerTask()
//{
//    _cancellationTokenSource.Cancel();
//}

//#endregion

