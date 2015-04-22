using UnityEngine;


[RequireComponent(typeof(ChatGUI))]
public class NamePickGui : MonoBehaviour
{
    public Vector2 GuiSize = new Vector2(200, 300);
    public string InputLine = string.Empty;

    private Rect _guiCenteredRect;
    private ChatGUI _chatComponent;
    private const string HelpText = "Welcome to the Photon Chat demo.\nEnter a nickname to start. This demo does not require users to authenticate.";
    private const string UserNamePlayerPref = "NamePickUserName";

    public void Awake()
    {
        _guiCenteredRect = new Rect(Screen.width / 2 - GuiSize.x / 2, Screen.height / 2 - GuiSize.y / 4, GuiSize.x, GuiSize.y);
        _chatComponent = GetComponent<ChatGUI>();

        if (_chatComponent != null && _chatComponent.enabled)
        {
            Debug.LogWarning("When using the NamePickGui, ChatGUI should be disabled initially.");

            if (_chatComponent.ChatClient != null)
            {
                _chatComponent.ChatClient.Disconnect();
            }
            _chatComponent.enabled = false;
        }

        var prefsName = PlayerPrefs.GetString(UserNamePlayerPref);
        if (!string.IsNullOrEmpty(prefsName))
        {
            InputLine = prefsName;
        }
    }

    public void OnGUI()
    {
        // Enter-Key handling:
        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(InputLine))
            {
                StartChat();
                return;
            }
        }


        GUI.skin.label.wordWrap = true;
        GUILayout.BeginArea(_guiCenteredRect);


        if (_chatComponent != null && string.IsNullOrEmpty(_chatComponent.ChatAppId))
        {
            GUILayout.Label("To continue, configure your Chat AppId.\nIt's listed in the Chat Dashboard (online).\nStop play-mode and edit:\nScripts/ChatGUI in the Hierarchy.");
            if (GUILayout.Button("Open Chat Dashboard"))
            {
                Application.OpenURL("https://www.exitgames.com/en/Chat/Dashboard");
            }
            GUILayout.EndArea();
            return;
        }

        GUILayout.Label(HelpText);

        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("NameInput");
        InputLine = GUILayout.TextField(InputLine);
        if (GUILayout.Button("Connect", GUILayout.ExpandWidth(false)))
        {
            StartChat();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndArea();


        GUI.FocusControl("NameInput");
    }

    private void StartChat()
    {
        _chatComponent.UserName = InputLine;
        _chatComponent.enabled = true;
        enabled = false;

        PlayerPrefs.SetString(UserNamePlayerPref, InputLine);
    }
}
