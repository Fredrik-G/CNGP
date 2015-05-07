using System;
using UnityEngine;

public class NetworkManager
{
    private const string UserNamePlayerPref = "NamePickUserName";

    public void Start()
    {
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
        {
            PhotonNetwork.ConnectUsingSettings("1.0");
        }

        var prefsName = PlayerPrefs.GetString(UserNamePlayerPref);
        if (String.IsNullOrEmpty(PhotonNetwork.playerName) && !String.IsNullOrEmpty(prefsName))
        {
            PhotonNetwork.playerName = prefsName;
        }

        // if you wanted more debug out, turn this on:
        // PhotonNetwork.logLevel = NetworkLogLevel.Full;
    }

    public void CreateNewLobby(string lobbyName)
    {
        PhotonNetwork.CreateRoom(lobbyName, new RoomOptions { maxPlayers = 10 }, null);
        Application.LoadLevel("Lobby");
    }

    public void JoinExistingLobby(string lobbyName)
    {
        PhotonNetwork.JoinRoom(lobbyName);
    }
}