using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Connect();
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnGUI()
    {
        var guiStyle = new GUIStyle {fontSize = 20};
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(), guiStyle);
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Failed to connect to a random room");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined random room");
    }
}
