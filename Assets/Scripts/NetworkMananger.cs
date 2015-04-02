using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkMananger : MonoBehaviour
{
    private readonly string _gameName = "CNGP-Server";
    private readonly float _refreshRequestLength = 3f;
    private HostData[] hostData;

    private void StartServer()
    {
        Network.InitializeServer(10, 23466, false);
        MasterServer.RegisterHost(_gameName, "Mega server osv", "CNGP server");
    }

    void OnServerInitialized()
    {
        Debug.Log("Server started");
    }

    void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registration successful");
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

        if (hostData != null)
        {
            GUI.Label(new Rect(40f, 75f, 150f, 30f), "No servers found");
            //for (var i = 0; i < hostData.Length; i++)
            //{
            //    //GUI.Button(new Rect(Screen.width/2, 65f + (30f * i), 300f, 30f), hostData[i].gameName);

            //}
        }
    }
}