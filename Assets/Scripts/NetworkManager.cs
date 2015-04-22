using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;
public class NetworkManager : MonoBehaviour
{
    public string RoomName = string.Empty;
    private SpawnSpots[] spawnSpots;
    // Use this for initialization
    void Start()
    {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpots>();
        if (!string.IsNullOrEmpty(MainMenuButtons.RoomName))
        {
            
            PhotonNetwork.CreateRoom(MainMenuButtons.RoomName, true, true, 4);
        }
        else
        {
            Connect();
        }
       
        
    }

    void Connect()
    {
        GUILayout.Label("Connecting...");
    }

    void OnGUI()
    {
       
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        
        PhotonNetwork.JoinRoom(RoomName);
    }

    void OnPhotonRandomJoinFailed()
    {
        GUILayout.Label("Failed to join server.");
        Application.LoadLevel("MainMenu");
        
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {

        SpawnSpots mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Ethan", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);


        Debug.Log(PhotonNetwork.player.GetTeam().ToString());
        
        ((MonoBehaviour)myPlayerGO.GetComponent("TopDownController")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("ProjectileShooter")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("PlayerStats")).enabled = true;
        var playerCamera = myPlayerGO.transform.FindChild("Main Camera").gameObject;
        ((MonoBehaviour)playerCamera.GetComponent("GameCamera")).enabled = true;
        //((MonoBehaviour)playerCamera.GetComponent("FixedRotation")).enabled = true;
        playerCamera.SetActive(true);
    }
}