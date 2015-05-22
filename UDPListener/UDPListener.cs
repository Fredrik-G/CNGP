using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            OpenNewListener();
        }

        #endregion

        #region ListenerView Events

        /// <summary>
        /// Occurs when the "TryingToStartNewListener"-event is raised from a "ListenerView".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerView_TryingToStartNewListener(object sender, EventArgs e)
        {
            PrepareToStartListener(sender);
        }

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
        /// Binds the "StartedNewListener"-event to the function ListenerView_StartedNewListener
        /// for the last inserted element in the ListenerView-list.
        /// </summary>
        private void AddNewListenerEventToLastListenerView()
        {
            _listenerViews[_listenerViews.Count - 1].StartedNewListener += ListenerView_StartedNewListener;
            _listenerViews[_listenerViews.Count - 1].StoppedListener += ListenerView_StoppedListener;
            _listenerViews[_listenerViews.Count - 1].TryingToStartNewListener += ListenerView_TryingToStartNewListener;
        }

        /// <summary>
        /// Opens a new ListenerView and adds it to the tab page.
        /// </summary>
        private void OpenNewListener()
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
        /// Prepares to start a listener.
        /// Makes sure its port is not already in use
        /// and starts if the port is available.
        /// </summary>
        /// <param name="sender">The ListenerView preparing to start.</param>
        private void PrepareToStartListener(object sender)
        {
            var listenerView = (ListenerView)sender;
            if (IsPortInUse(listenerView))
            {
                MessageBox.Show("Port is already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                listenerView.StartListening();
            }
        }

        /// <summary>
        /// Checks if the port of the new ListenerView is already in use.
        /// Goes through every active listener and compares their port to the new listener.
        /// </summary>
        /// <param name="newListenerView">The new listener to check port usage for.</param>
        /// <returns>Returns true if given port is already in use.</returns>
        private bool IsPortInUse(ListenerView newListenerView)
        {
            return _listenerViews.Where(x => x.UdpListenIsActive).Any(oldListener => oldListener.Port == newListenerView.Port);
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
        /// Occurs when the OpenNewListener-menu is clicked in the toolstrip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openNewListenerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenNewListener();
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

        #region Tab Page Events

        /// <summary>
        /// Occurs when painting/drawing a tab page.
        /// Sets the caption of a TabItem in the ListenerTabControl to its name + "x". 
        /// Creates a "x" at the end of a Tab caption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(ListenerTabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12,
                e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Occurs when the ListenerTabControl is clicked.
        /// Allows the user to close a tab if the "x" was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListenerTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < ListenerTabControl.TabPages.Count; i++)
            {
                var tabRect = ListenerTabControl.GetTabRect(i);
                //Getting the position of the "x" mark.
                var closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                    if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ListenerTabControl.TabPages.RemoveAt(i);
                        _listenerViews[i].StopListening();
                        _listenerViews.RemoveAt(i);
                        UpdateInfoFormTitle();
                        break;
                    }
            }
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

