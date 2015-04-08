using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkMananger : MonoBehaviour
{
    private readonly string _gameName = "CNGP-Server2";
    private readonly float _refreshRequestLength = 3f;
    private HostData[] hostData;

    private void StartServer()
    {
        Network.InitializeServer(10, 50005, false);
        MasterServer.RegisterHost(_gameName, "Mega server osv2", "CNGP server2");
    }

    private void SpawnPlayer()
    {
        Debug.Log("Spawning player");
        Network.Instantiate(
            Resources.Load("Prefabs/TestPlayer"),
            new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server started");
        SpawnPlayer();
    }
    void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registration successful");
        }
    }
    void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect to master server" + error);
    }
    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Failed to connect." + error);
    }
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        Debug.Log("Network Instantiated" + info.sender);
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected.");
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player disconnected." + player.ipAddress);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
    void OnConnectedToServer()
    {
        Debug.Log("Connected to server.");
    }
    void OnDisconnectedFromServer(NetworkDisconnection error)
    {
        Debug.Log("Disconnected from server");
    }

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

    public IEnumerator RefreshHostList()
    {
        Debug.Log("Refreshing");
        MasterServer.RequestHostList(_gameName);

        var endTime = Time.time + _refreshRequestLength;

        while (Time.time < endTime)
        {
            hostData = MasterServer.PollHostList();
            yield return new WaitForEndOfFrame();
        }

        if (hostData == null || hostData.Length == 0)
        {
            GUI.Label(new Rect(40, 20f, 150f, 30f), "No servers found");
            Debug.Log("No active servers were found.");
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
        }

        if(Network.isClient)
        {
            if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Spawn"))
            {
                SpawnPlayer();
            }
        }

        if (!Network.isServer && !Network.isClient)
        {
            if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Start server"))
            {
                StartServer();
            }
            if (GUI.Button(new Rect(25f, 55f, 150f, 30f), "Refresh"))
            {
                StartCoroutine("RefreshHostList");
            }

            if (hostData != null)
            {
                for (var i = 0; i < hostData.Length; i++)
                {
                    if (GUI.Button(new Rect(Screen.width / 2, 65f + (30f * i), 300f, 30f), hostData[i].gameName))
                    {
                        Network.Connect(hostData[i]);
                        Debug.Log("Connected to server " + hostData[i].gameName);
                    }

                }
            }
        }
    }


}