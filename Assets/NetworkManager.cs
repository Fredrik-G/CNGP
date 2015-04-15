using UnityEngine;
using System.Collections;
using Engine;
public class NetworkManager : MonoBehaviour
{
    public Camera standbyCamera;
    public string RoomName = string.Empty;

    void Start()
    {
        Connect();
    }

    void Connect()
    {
    
        Debug.Log("Connecting...");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if(PhotonNetwork.inRoom)
        {
            return;
        }

        if(string.IsNullOrEmpty(RoomName))
        {
            GUI.Label(UIFormat.CreateCenteredRect(0), "Server Name:");
        }

        RoomName = GUI.TextField(UIFormat.CreateCenteredRect(0), RoomName, 20);
        if (GUI.Button(UIFormat.CreateCenteredRect(-20), "OK") && !string.IsNullOrEmpty(RoomName))
        {
            PhotonNetwork.CreateRoom(RoomName);
        }       

    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");

        PhotonNetwork.JoinRoom(RoomName);
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom ");
        
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        Debug.Log("SPAWN");
        GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Ethan", new Vector3(0,0,0),new Quaternion(0,0,0,0), 0);
        ((MonoBehaviour)myPlayerGO.GetComponent("TopDownController")).enabled = true;
        myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
        
    }
}
