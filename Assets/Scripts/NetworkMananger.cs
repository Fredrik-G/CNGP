using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Engine;

public class NetworkMananger : MonoBehaviour
{
    #region Data

    public Transform PlayerPrefab;
    public NetworkPlayer Player;

    private readonly float _refreshRequestLength = 3.0f;
    private readonly string _registeredName = "CNGPspel";
    public string InputGameName = String.Empty;
    public int MaxPlayers = 10;

    private HostData[] _hostData;

    #endregion
      
    /// <summary>
    /// Starts a new server and register it with the Unity Master Server.
    /// </summary>
    private void StartServer()
    {
        Network.InitializeServer(MaxPlayers, 50005, false);
        MasterServer.RegisterHost(_registeredName, InputGameName);
    }

    void OnServerInitialized()
    {
        if (Network.isServer)
        {
            Player = Network.player;
            MakePlayer(Player);
        }
    }

    void OnConnectedToServer()
    {
        Player = Network.player;
        GetComponent<NetworkView>().RPC("MakePlayer", RPCMode.Server, Player);
    }

    /// <summary>
    /// Creates and instantiates a new player.
    /// </summary>
    /// <param name="player">The player to instantiate</param>
    [RPC]
    void MakePlayer(NetworkPlayer player)
    {
        //Instantiates a new player on the network.
        var newPlayer = Network.Instantiate(PlayerPrefab, new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0) as Transform;

        //Checks if the sent player is a server.
        if (player == Player)
        {
            EnableCamera(newPlayer.GetComponent<NetworkView>().viewID);           
        }
        else
        {
            GetComponent<NetworkView>().RPC("EnableCamera", player, newPlayer.GetComponent<NetworkView>().viewID);
        }
    }

    /// <summary>
    /// Enables the camera for a player.
    /// </summary>
    /// <param name="playerId"></param>
    [RPC]
    void EnableCamera(NetworkViewID playerId)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        //Goes through every player until it finds one with the same ID and enables his camera.
        foreach (var player in players.Where(player => player.GetComponent<NetworkView>().viewID == playerId))
        {
            player.GetComponent<Movement>().HaveControl = true;
            var myCamera = player.transform.Find("Camera");
            myCamera.GetComponent<Camera>().enabled = true;
            myCamera.GetComponent<Camera>().GetComponent<AudioListener>().enabled = true;
            break;
        }
    }

    /// <summary>
    /// Refreshs the host list using Unity Master Server.
    /// </summary>
    /// <returns></returns>
    public IEnumerator RefreshHostList()
    {
        Debug.Log("Refreshing");
        MasterServer.RequestHostList(_registeredName);
        var endTime = Time.time + _refreshRequestLength;

        while (Time.time < endTime)
        {
            _hostData = MasterServer.PollHostList();
            yield return new WaitForEndOfFrame();
        }

        if (_hostData == null || _hostData.Length == 0)
        {
            Debug.Log("No active servers were found.");
        }
    }

    public void OnGUI()
    {
        if (Network.isClient || Network.isServer)
        {
            return;
        }

        if (String.IsNullOrEmpty(InputGameName))
        {
            GUI.Label(UIFormat.CreateCenteredRect(0), "Game Name");
        }

        InputGameName = GUI.TextField(UIFormat.CreateCenteredRect(0), InputGameName);

        if (GUI.Button(UIFormat.CreateCenteredRect(-20), "Start New Server"))
        {
            if (!String.IsNullOrEmpty(InputGameName))
            {
                StartServer();
            }
        }

        if (GUI.Button(UIFormat.CreateCenteredRect(-40), "Find Servers"))
        {
            StartCoroutine(RefreshHostList());
        }

        ShowServerList();
    }

    /// <summary>
    /// Displays all servers inside the Unity Master Server.
    /// </summary>
    private void ShowServerList()
    {
        if (_hostData == null) return;

        for(var i = 0; i<_hostData.Length; i++)
        {
            if (GUI.Button(UIFormat.CreateCenteredRect(-i*10-60), _hostData[i].gameName))
            {
                Network.Connect(_hostData[i]);
            }
        }
    }
}