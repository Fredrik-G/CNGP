using UnityEngine;
using System.Collections;

public class EnableChat : MonoBehaviour
{
    public string AccountName = string.Empty;

    private ChatGUI _chatComponent;
    private const string UserNamePlayerPref = "NamePickUserName";

    public void Awake()
    {
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
            AccountName = prefsName;
        }

        StartChat();
    }

    private void StartChat()
    {
        _chatComponent.UserName = AccountName;
        _chatComponent.enabled = true;
        enabled = false;

        PlayerPrefs.SetString(UserNamePlayerPref, AccountName);
    }
}
