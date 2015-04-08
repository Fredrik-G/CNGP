﻿using System.Collections;
using UnityEngine;

public class NetworkMananger : MonoBehaviour
{
    #region Data

    private readonly string _gameName = "CNGP-Server";
    private readonly float _refreshRequestLength = 3f;
    private HostData[] _hostData;

    #endregion
   
    #region Server Events

    private void OnServerInitialized()
    {
        Debug.Log("Server started");
        SpawnPlayer();
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected to server.");
    }

    private void OnDisconnectedFromServer(NetworkDisconnection error)
    {
        Debug.Log("Disconnected from server");
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect to server." + error);
    }

    #endregion

    #region Master Server Events

    private void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        Debug.Log(masterServerEvent == MasterServerEvent.RegistrationSucceeded
            ? "Registration successful"
            : "Registration unsuccessful");
    }

    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect to master server" + error);
    }

    /// <summary>
    /// Called on objects which have been network instantiated with Network.Instantiate.
    /// </summary>
    /// <param name="info"></param>
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        Debug.Log("Network Instantiated" + info.sender);
    }

    #endregion

    #region Player Events

    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected.");
        // UpdatePlayerList();
    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player disconnected." + player.ipAddress);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);

        // UpdatePlayerList();
    }

    #endregion

    #region Application Events

    void OnApplicationQuit()
    {
        if (Network.isServer)
        {
            Network.Disconnect(200);
            MasterServer.UnregisterHost();

            Debug.Log("Disconnected server.");
        }
        if (Network.isClient)
        {
            Network.Disconnect(200);
        }
    }

    public void OnGUI()
    {
        if (Network.isServer)
        {
            GUILayout.Label("Running as a server.");
        }
        else if (Network.isClient)
        {
            GUILayout.Label("Running as a client.");
            if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Spawn"))
            {
                SpawnPlayer();
            }
        }

        // UpdatePlayerList();

        if (Network.isServer || Network.isClient)
        {
            return;
        }
        if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Start server"))
        {
            StartServer();
        }
        if (GUI.Button(new Rect(25f, 55f, 150f, 30f), "Refresh"))
        {
            StartCoroutine("RefreshHostList");
        }

        ShowServerList();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Starts a new server and registers it with Unity Master Server.
    /// </summary>
    private void StartServer()
    {
        Network.InitializeServer(10, 50005, false);
        MasterServer.RegisterHost(_gameName, "Mega server osv", "CNGP server");
    }

    /// <summary>
    /// Loads a prefab and instantiate it in the network.
    /// </summary>
    private void SpawnPlayer()
    {
        Debug.Log("Spawning player");
        Network.Instantiate(
            Resources.Load("Prefabs/TestPlayer"),
            new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0);
    }

    /// <summary>
    /// Returns all registered servers in the master server.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RefreshHostList()
    {
        Debug.Log("Refreshing");
        MasterServer.RequestHostList(_gameName);

        var endTime = Time.time + _refreshRequestLength;

        while (Time.time < endTime)
        {
            _hostData = MasterServer.PollHostList();
            yield return new WaitForEndOfFrame();
        }

        if (_hostData == null || _hostData.Length == 0)
        {
            GUI.Label(new Rect(40, 20f, 150f, 30f), "No servers found");
            Debug.Log("No active servers were found.");
        }
    }

    /// <summary>
    /// Creates a button for each server
    /// and lets the user connect by clicking on it.
    /// </summary>
    private void ShowServerList()
    {
        if (_hostData == null)
        {
            return;
        }
        for (var i = 0; i < _hostData.Length; i++)
        {
            if (GUI.Button(new Rect(Screen.width/2, 65f + (30f*i), 300f, 30f), _hostData[i].gameName))
            {
                Network.Connect(_hostData[i]);
                Debug.Log("Connected to server " + _hostData[i].gameName);
            }
        }
    }

    /// <summary>
    /// Displays current players in a list.
    /// </summary>
    private void UpdatePlayerList()
    {
        var i = 0;
        Debug.Log("Updating player list");
        foreach (var player in Network.connections)
        {
            Debug.Log("asd");
            var playerInfo = player.guid + " " + player.ipAddress + " " + player.externalIP;
            GUI.Label(new Rect(Screen.width/2, 20*i++, 400, 50), playerInfo);
        }
    }

    #endregion

}