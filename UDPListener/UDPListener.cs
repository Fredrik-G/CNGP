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

        /// <summary>
        /// Default constructor
        /// </summary>
        public UDPListener()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the form is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UDPListener_Load(object sender, EventArgs e)
        {
            StartNewListener();
        }

        #endregion

        #region ListenerView Events

        /// <summary>
        /// Occurs when the "StartedNewListener"-event is raised from a "ListenerView".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerView_StartedNewListener(object sender, EventArgs e)
        {
            UpdateInfoFormTitle();
        }

        /// <summary>
        /// Occurs when the "StoppedListener"-event is raised from a "ListenerView".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerView_StoppedListener(object sender, EventArgs e)
        {
            UpdateInfoFormTitle();
        }

        #endregion

        /// <summary>
        /// Updates the form title based on current listeners.
        /// </summary>
        private void UpdateInfoFormTitle()
        {
            var numberOfActiveListeners = _listenerViews.Count(listenerView => listenerView.UdpListenIsActive);
            var totalNumberOfListeners = _listenerViews.Count;
            Text = String.Format("{0} Listener(s) Up  -  {1}/{2} Active Listeners", totalNumberOfListeners,
                numberOfActiveListeners, totalNumberOfListeners);
        }

        #region Listener Methods

        /// <summary>
        /// Starts a new ListenerView and adds it to the tab page.
        /// </summary>
        private void StartNewListener()
        {
            var listenerView = new ListenerView
            {
                Size = Size,
                Dock = DockStyle.Fill
                //  Anchor = AnchorStyles.Bottom | AnchorStyles.Right |AnchorStyles.Top | AnchorStyles.Left
            };
            var numerOfTabs = ListenerTabControl.TabPages.Count + 1;
            var tabPage = new TabPage { Text = "Listener " + numerOfTabs };
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

        /// <summary>
        /// Stops all listeners.
        /// </summary>
        private void StopAllListeners()
        {
            foreach (var listenerView in _listenerViews)
            {
                listenerView.StopListening();
            }
        }

        #endregion

        #region ToolStipMenu Clicks

        /// <summary>
        /// Occurs when the StartNewListener-menu is clicked in the toolstrip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startNewListenerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            StartNewListener();
        }

        /// <summary>
        /// Occurs when the StopALlListeners-menu is clicked in the toolstrip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

