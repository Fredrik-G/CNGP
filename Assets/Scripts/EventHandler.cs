using System;
using Engine;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    //public Image Image;
    public InputField InputField;
    public string RoomName = string.Empty;
    private NetworkManager _networkManager = new NetworkManager();

    private Vector2 scrollPos = Vector2.zero;
    public Vector2 WidthAndHeight = new Vector2(300, 10);
    private readonly Rect _roomInfoRect = new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 2 + Screen.height / 20, Screen.width / 3, Screen.height / 2);

    private enum Menus
    {
        Default,
        StartNewLobby,
        JoinLobby,
        Options
    }

    private Menus _currentMenu = Menus.Default;

    public void Start()
    {
        //   Image.enabled = false;
        DisableInputField();
        _networkManager.Start();
    }

    public void OnGUI()
    {
        switch (_currentMenu)
        {
            case Menus.Default:
                return;
            case Menus.StartNewLobby:
                StartNewLobbyGui();
                break;
            case Menus.JoinLobby:
                JoinLobbyGui();
                break;   
            case Menus.Options:
                OptionsGui();
                break;
        }
    }

    private void OptionsGui()
    {
        //TODO
    }

    private void StartNewLobbyGui()
    {       
        if (InputField.isFocused && !String.IsNullOrEmpty(InputField.text) && Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Starting new lobby with name " + InputField.text);
            _networkManager.CreateNewLobby(InputField.text);
        }
    }

    private void JoinLobbyGui()
    {   
        DisplayAllRooms();
    }

    #region Mouse Hover

    public void HandleStartNewLobbyHover()
    {
        GameObject.FindGameObjectWithTag("StartNewLobbyImage").GetComponent<Image>().enabled = true;
    }

    public void HandleJoinLobbyHover()
    {
        GameObject.FindGameObjectWithTag("JoinLobbyImage").GetComponent<Image>().enabled = true;
    }

    public void HandleOptionsHover()
    {
        GameObject.FindGameObjectWithTag("OptionsImage").GetComponent<Image>().enabled = true;
    }

    public void HandleMouseLeave()
    {
        GameObject.FindGameObjectWithTag("StartNewLobbyImage").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("JoinLobbyImage").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("OptionsImage").GetComponent<Image>().enabled = false;
    }

    #endregion

    #region Mouse Clicks

    public void HandleStartNewLobbyClick()
    {
        ResetGui();
        EnableInputField();
        _currentMenu = Menus.StartNewLobby;
    }

    public void HandleJoinLobbyClick()
    {
        ResetGui();
        _currentMenu = Menus.JoinLobby;
    }

    public void HandleOptionsClick()
    {
        ResetGui();
        _currentMenu = Menus.Options;
    }

    #endregion

    private void DisplayAllRooms()
    {
        GUI.Box(_roomInfoRect, "Join Lobby");
        GUILayout.BeginArea(_roomInfoRect);

        GUILayout.Space(15);
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("Currently no games are available.");
            GUILayout.Label("Rooms will be listed here when they become available.");
        }
        else
        {
            GUILayout.Label(PhotonNetwork.GetRoomList().Length + " rooms available:");

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach (var roomInfo in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
                if (GUILayout.Button("Join", GUILayout.Width(150)))
                {
                    PhotonNetwork.JoinRoom(roomInfo.name);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
    }

    private void DisableInputField()
    {
        InputField.enabled = false;
        InputField.GetComponent<Image>().enabled = false;
        InputField.GetComponent<InputField>().enabled = false;

        InputField.GetComponentInChildren<Text>().enabled = false;
    }

    private void EnableInputField()
    {
        InputField.enabled = true;
        InputField.GetComponent<Image>().enabled = true;
        InputField.GetComponent<InputField>().enabled = true;

        InputField.GetComponentInChildren<Text>().enabled = true;
    }

    private void ResetGui()
    {
        DisableInputField();
        InputField.text = String.Empty;
    }

}
