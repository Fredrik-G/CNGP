using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.Chat;
using UnityEngine;

/// <summary>
/// This simple Chat Server Gui uses a global chat (in lobby) and a room chat (in room).
/// </summary>
/// <remarks>
/// The Chat Server API in ChatClient basically lets you create any number of channels. 
/// You just have to name them. Example: "gc" for Global Channel or for rooms: "rc"+RoomName.GetHashCode()
/// 
/// This simple demo sends in a global chat when in lobby and in room channel when in room.
/// Create a more elaborate UI to let players chat in either channel while in room or send private messages.
/// 
/// Names of users are set in Authenticate. That should be unique so users can actually get their messages.
/// 
/// 
/// Workflow: 
/// Create ChatClient, Connect to a server with your AppID, Authenticate the user (apply a unique name)
/// and subscribe to some channels. 
/// Subscribe a channel before you publish to that channel!
/// 
/// 
/// Note: 
/// Don't forget to call ChatClient.Service(). Might later on be integrated into PUN but for now don't forget.
/// </remarks>
public class ChatGUI : MonoBehaviour, IChatClientListener
{
    //Photon chat ID. Used when connecting to Photon Chat API.
    public string ChatAppId = "5e4a6881-66f5-41a4-bee5-f7397d719e50";
    //Channels to join automatically.
    public string[] ChannelsToJoinOnConnect = { "Team", "Global" };
    //Up to a certain degree, previously sent messages can be fetched for context.
    public int HistoryLengthToFetch;
    //Puts the chat in lobbymode -> does not fade out chat.
    public bool LobbyMode = true;

    public string UserName { get; set; }

    private ChatChannel _selectedChannel;
    private string _selectedChannelName;     // mainly used for GUI/input
    private int _selectedChannelIndex;   // mainly used for GUI/input
    bool _doingPrivateChat;

    //Determines if chat should be in debugmode and print debug text.
    public bool DebugMode = true;

    //Timeout stuff
    private bool _displayMessages = true;
    private Single _timeAtLastMessage;
    public float TimeoutDelay = 5f;

    public ChatClient ChatClient;

    //GUI stuff
    public Rect GuiRect = new Rect(0, 0, 250, 300);
    public bool IsVisible = true;
    public bool AlignBottom = false;
    public bool FullScreen = false;

    private string _inputLine = String.Empty;
    private string _userIdInput = String.Empty;
    private Vector2 _scrollPos = Vector2.zero;
    private const string WelcomeText = "Welcome to chat. /help lists commands.";
    private const string HelpText = "/join <list of channelnames> joins channels.\n" +
                                    "/leave <list of channelnames> leaves channels.\n" +
                                    "/w <username> <message> send private message to user.\n" +
                                    "/clear clears the current chat tab. private chats get closed.\n" +
                                    "/help displays this help message.";

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true; // this must run in background or it will drop connection if not focussed.

        if (string.IsNullOrEmpty(UserName))
        {
            UserName = "user" + Environment.TickCount % 99; //made-up username
        }

        ChatClient = new ChatClient(this);
        ChatClient.Connect(ChatAppId, "1.0", UserName, null);

        DebugText("User " + UserName + "connected to chat client");

        if (AlignBottom)
        {
            GuiRect.y = Screen.height - GuiRect.height;
        }
        if (FullScreen)
        {
            GuiRect.x = 0;
            GuiRect.y = 0;
            GuiRect.width = Screen.width;
            GuiRect.height = Screen.height;
        }

        Debug.Log(UserName);
    }

    public void Update()
    {
        if (ChatClient != null)
        {
            ChatClient.Service();  // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }

    public void OnGUI()
    {
        //Checks if chat should be displayed (timeout)
        if (Time.time - _timeAtLastMessage > TimeoutDelay && String.IsNullOrEmpty(_inputLine) && !LobbyMode)
        {
            _displayMessages = false;
        }
        else
        {
            _displayMessages = true;
        }

        if (!IsVisible)
        {
            return;
        }

        GUI.skin.label.wordWrap = true;

        //Checks for ENTER and either opens chat input or sends current msg.
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
        {
            if ("ChatInput".Equals(GUI.GetNameOfFocusedControl()))
            {
                // focus on input -> submit it
                GuiSendsMessage();
                return; // displaying the now modified list would result in an error. to avoid this, we just skip this single frame
            }

            _displayMessages = true;
            _timeAtLastMessage = Time.time;
            GUI.FocusControl("ChatInput");
            return;
        }

        //Checks for ESCAPE and removes focus from chat input.
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Escape))
        {
            if ("ChatInput".Equals(GUI.GetNameOfFocusedControl()))
            {
                _inputLine = String.Empty;
                GUI.FocusControl(String.Empty);
            }
        }

        GUI.SetNextControlName(String.Empty);
        GUILayout.BeginArea(GuiRect);

        GUILayout.FlexibleSpace();

        if (ChatClient.State != ChatState.ConnectedToFrontEnd)
        {
            GUILayout.Label("Not in chat.");
        }
        else
        {
            DisplayChat();
        }

        GUILayout.BeginHorizontal();

        if (_doingPrivateChat)
        {
            DisplayPrivateMessageChat();
        }

        if (_displayMessages)
        {
            DisplayInputField();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Displays everything regarding the chat.
    /// </summary>
    private void DisplayChat()
    {
        var channels = new List<string>(ChatClient.PublicChannels.Keys);
        var countOfPublicChannels = channels.Count;
        channels.AddRange(ChatClient.PrivateChannels.Keys);

        if (countOfPublicChannels <= 0)
        {
            return;
        }

        DisplayChatChannel(channels, countOfPublicChannels);

        if (_displayMessages)
        {
            GUILayout.Label(WelcomeText);

            if (ChatClient.TryGetChannel(_selectedChannelName, _doingPrivateChat, out _selectedChannel))
            {
                DisplayChatMessages();
            }
        }

        GUILayout.EndScrollView();
    }

    /// <summary>
    /// Displays every joined channel and allows users to click on a channel to change focus to it.
    /// </summary>
    /// <param name="channels"></param>
    /// <param name="countOfPublicChannels"></param>
    private void DisplayChatChannel(List<string> channels, int countOfPublicChannels)
    {
        var previouslySelectedChannelIndex = _selectedChannelIndex;
        var channelIndex = channels.IndexOf(_selectedChannelName);
        _selectedChannelIndex = (channelIndex >= 0) ? channelIndex : 0;

        _selectedChannelIndex = GUILayout.Toolbar(_selectedChannelIndex, channels.ToArray(),
            GUILayout.ExpandWidth(false));
        _scrollPos = GUILayout.BeginScrollView(_scrollPos);

        _doingPrivateChat = (_selectedChannelIndex >= countOfPublicChannels);
        _selectedChannelName = channels[_selectedChannelIndex];

        if (_selectedChannelIndex != previouslySelectedChannelIndex)
        {
            // changed channel -> scroll down, if private: pre-fill "to" field with target user's name
            _scrollPos.y = float.MaxValue;
            if (_doingPrivateChat)
            {
                var pieces = _selectedChannelName.Split(new char[] { ':' }, 3);
                _userIdInput = pieces[1];
            }
        }
    }

    /// <summary>
    /// Displays the input field that lets the user enter a message.
    /// </summary>
    private void DisplayInputField()
    {
        GUI.SetNextControlName("ChatInput");
        _inputLine = GUILayout.TextField(_inputLine);
        if (GUILayout.Button("Send", GUILayout.ExpandWidth(false)))
        {
            GuiSendsMessage();
        }
    }

    /// <summary>
    /// Displays an input field for private messaging.
    /// </summary>
    private void DisplayPrivateMessageChat()
    {
        GUILayout.Label("to:", GUILayout.ExpandWidth(false));
        GUI.SetNextControlName("WhisperTo");
        _userIdInput = GUILayout.TextField(_userIdInput, GUILayout.MinWidth(100), GUILayout.ExpandWidth(false));
        var focussed = GUI.GetNameOfFocusedControl();
        if (focussed.Equals("WhisperTo"))
        {
            if (_userIdInput.Equals("username"))
            {
                _userIdInput = String.Empty;
            }
        }
        else if (string.IsNullOrEmpty(_userIdInput))
        {
            _userIdInput = "username";
        }
    }

    /// <summary>
    /// Displays all messages in the selected channel.
    /// </summary>
    private void DisplayChatMessages()
    {
        for (var i = 0; i < _selectedChannel.Messages.Count; i++)
        {
            var sender = _selectedChannel.Senders[i];
            var message = _selectedChannel.Messages[i];
            GUILayout.Label(string.Format("{0}: {1}", sender, message));
        }
    }

    /// <summary>
    /// Occurs when a new message was sent.
    /// </summary>
    /// <param name="channelName"></param>
    /// <param name="senders"></param>
    /// <param name="messages"></param>
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(_selectedChannelName))
        {
            Debug.Log("Nytt msg");
            _scrollPos.y = float.MaxValue;
            _timeAtLastMessage = Time.time;
            _displayMessages = true;
        }
    }

    /// <summary>
    /// Sends a message based on the input field.
    /// </summary>
    private void GuiSendsMessage()
    {
        if (string.IsNullOrEmpty(_inputLine))
        {
            GUI.FocusControl(String.Empty);
            return;
        }

        //Checks if user input special command
        if (_inputLine[0].Equals('/'))
        {
            InterpretSpecialCommand();
        }
        else
        {
            if (_doingPrivateChat)
            {
                ChatClient.SendPrivateMessage(_userIdInput, _inputLine);
                DebugText("Sent private message with content: " + _inputLine);
            }
            else
            {
                ChatClient.PublishMessage(_selectedChannelName, _inputLine);
                DebugText("Sent message with content: " + _inputLine);
            }
        }

        _inputLine = String.Empty;
        GUI.FocusControl(String.Empty);
    }

    /// <summary>
    /// Displays help string to the selected channel.
    /// </summary>
    private void PostHelpToCurrentChannel()
    {
        var channelForHelp = _selectedChannel;
        if (channelForHelp != null)
        {
            channelForHelp.Add("info", HelpText);
        }
        else
        {
            Debug.LogError("no channel for help");
        }
    }

    /// <summary>
    /// Interprets special commands like /help, /join.
    /// </summary>
    private void InterpretSpecialCommand()
    {
        var tokens = _inputLine.Split(new char[] { ' ' }, 2);
        if (tokens[0].Equals("/help"))
        {
            PostHelpToCurrentChannel();
        }
        else if (tokens[0].Equals("/join") && !string.IsNullOrEmpty(tokens[1]))
        {
            ChatClient.Subscribe(tokens[1].Split(' ', ','));
        }
        else if (tokens[0].Equals("/leave") && !string.IsNullOrEmpty(tokens[1]))
        {
            var inputedChannels = tokens[1].Split(' ', ',');
            //Makes sure user doesn't leave Team or Global.
            if (inputedChannels.Any(channel => channel == "Team" || channel == "Global"))
            {
                _inputLine = String.Empty;
                return;
            }
            ChatClient.Unsubscribe(inputedChannels);
        }
        else if (tokens[0].Equals("/t") && !string.IsNullOrEmpty(tokens[1]))
        {
            var inputLine = tokens[1].Split(' ');
            var message = inputLine[0];
            ChatClient.PublishMessage("Team", message);
        }
        else if (tokens[0].Equals("/g") && !string.IsNullOrEmpty(tokens[1]))
        {
            var inputLine = tokens[1].Split(' ');
            var message = inputLine[0];
            ChatClient.PublishMessage("Global", message);
        }

        else if (tokens[0].Equals("/clear"))
        {
            if (_doingPrivateChat)
            {
                ChatClient.PrivateChannels.Remove(_selectedChannelName);
            }
            else
            {
                ChatChannel channel;
                if (ChatClient.TryGetChannel(_selectedChannelName, _doingPrivateChat, out channel))
                {
                    channel.ClearMessages();
                }
            }
        }
        else if (tokens[0].Equals("/w") && !string.IsNullOrEmpty(tokens[1]))
        {
            var subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);
            var targetUser = subtokens[0];
            var message = subtokens[1];
            ChatClient.SendPrivateMessage(targetUser, message);
        }
    }

    public void OnConnected()
    {
        if (ChannelsToJoinOnConnect != null && ChannelsToJoinOnConnect.Length > 0)
        {
            ChatClient.Subscribe(ChannelsToJoinOnConnect, HistoryLengthToFetch);
        }

        ChatClient.AddFriends(new string[] { "tobi", "ilya" });          // Add some users to the server-list to get their status updates
        ChatClient.SetOnlineStatus(ChatUserStatus.Online);             // You can set your online state (without a mesage).
    }

    public void OnDisconnected()
    {
    }

    /// <summary>
    /// To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnApplicationQuit.
    /// </summary>
    public void OnApplicationQuit()
    {
        if (ChatClient != null)
        {
            ChatClient.Disconnect();
        }
    }

    public void OnChatStateChange(ChatState state)
    {
        // use OnConnected() and OnDisconnected()
        // this method might become more useful in the future, when more complex states are being used.
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        _timeAtLastMessage = Time.time;
        foreach (var channel in channels)
        {
            ChatClient.PublishMessage(channel, "says 'hi' in OnSubscribed()."); // you don't HAVE to send a msg on join but you could.
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        // as the ChatClient is buffering the messages for you, this GUI doesn't need to do anything here
        // you also get messages that you sent yourself. in that case, the channelName is determinded by the target of your msg
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        // this is how you get status updates of friends.
        // this demo simply adds status updates to the currently shown chat.
        // you could buffer them or use them any other way, too.

        var activeChannel = _selectedChannel;
        if (activeChannel != null)
        {
            activeChannel.Add("info", string.Format("{0} is {1}. Msg:{2}", user, status, message));
        }

        Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));
    }

    /// <summary>
    /// Logs debug text if DebugMode is true.
    /// </summary>
    /// <param name="text"></param>
    private void DebugText(string text)
    {
        if (DebugMode)
            Debug.Log(text);
    }
}