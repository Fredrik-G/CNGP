using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;
public class MainMenuButtons : MonoBehaviour {
    public GUIStyle Style;
    private RoomInfo[] _roomList;
    public static string RoomName = string.Empty;
    public GameObject inputField;
	void Start () {
        PhotonNetwork.ConnectUsingSettings("CNGPv1.0"); 
        //PhotonNetwork.ConnectToMaster("127.0.0.1", 5058, "aa16c020-caf0-42f9-9350-985b9d48001a", "CNGPv1.0");
	}
	

	void Update () {
	
	}
    public void ExitApplication()
    {
        Application.Quit();
    }
    public void OnGUI()
    {
        if (_roomList == null)
        {
            return;
        }
        for (var i = 0; i < _roomList.Length; i++)
        {
            if (GUI.Button(UIFormat.CreateRightRect(-50), _roomList[i].name + " " + _roomList[i].playerCount + "/" + _roomList[i].maxPlayers, Style))
            {

                
                PhotonNetwork.JoinRoom(_roomList[i].name);
                Application.LoadLevel("De_dust");
            }
        }
    }
    public void ShowServerList()
    {
        _roomList = PhotonNetwork.GetRoomList();
    }

    public void CreateServer()
    {
        if (!inputField.active)
        {
            inputField.SetActive(true);
        }
        else
        {
            DontDestroyOnLoad(this);
            InputField fieldText = inputField.GetComponent<InputField>();
            RoomName = fieldText.text;
            Application.LoadLevel("De_dust");
        }
    }
    void OnDisconnectedFromPhoton()
    {
        print("Player disconnected");
    }
    public void JoinFoundry()
    {
        Application.LoadLevel("Foundry");
    }
}
