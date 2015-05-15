using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;

public class NetworkManager : MonoBehaviour
{
    bool hasPickedTeam = false;
    private int teamID;
    public string RoomName = string.Empty;
    private SpawnSpots[] spawnSpots;
    public GameObject backgroundCamera;
    private bool _escapePressed;
    private GameObject matchController;
    #region Respawntimer and dead
    public float respawnTimer = 0;
    private bool deathAdded = false;
    public bool isDead = false;
    public bool killsAdded = false;
    #endregion

    #region GUI
    public GUIStyle deathCamTextStyle;
    public GUIStyle deathCamButtonStyle;
    #endregion
    public int GetTeamID()
    {
        return teamID;
    }
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
    }
    
    void OnGUI()
    {
      
            if (isDead)
            {
                if((matchController.GetComponent<MatchController>().PointsBlueTeam >= 100) ||
                    (matchController.GetComponent<MatchController>().PointsRedTeam >= 100))
                {
                    SpawnMyPlayer();
                    this.isDead = false;
                }
                else
                {
                    if (!_escapePressed)
                    {
                        GUILayout.TextArea("You are dead.", deathCamTextStyle);
                        if (respawnTimer <= 0)
                        {
                            if (GUI.Button(UIFormat.CreateCenteredRect(0), "Respawn", deathCamButtonStyle))
                            {
                                SpawnMyPlayer();
                            }

                        }
                    }
            }
        }
        if (_escapePressed)
        {
            if (GUI.Button(UIFormat.CreateCenteredRect(40), "FPS counter", deathCamButtonStyle))
            {
                MonoBehaviour fpsCounter = ((MonoBehaviour)this.gameObject.GetComponent("FPSCounter"));
                fpsCounter.enabled = !fpsCounter.enabled;
            }
            if (GUI.Button(UIFormat.CreateCenteredRect(0), "Leave game", deathCamButtonStyle))
            {
                
                PhotonNetwork.LeaveRoom();
                Application.LoadLevel("MainMenu");
            }
        }
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        if(string.IsNullOrEmpty(RoomName))
        {
            PhotonNetwork.JoinRoom(RoomName);
        }
        else
        {
            Debug.Log("Failed to join room");
            Application.LoadLevel("MainMenu");
        }
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Failed to join server.");
        Application.LoadLevel("MainMenu");
        
    }

    void OnJoinedRoom()
    {
        var temp = 0;
        backgroundCamera.SetActive(true);
        Debug.Log("OnJoinedRoom");
        if (PhotonNetwork.room.playerCount % 2 == 0)
        {
            temp = 1;
        }
        else
        {
            temp = 0;
        }
        ExitGames.Client.Photon.Hashtable hs = new ExitGames.Client.Photon.Hashtable();
        hs.Add("teamID", temp);
        hs.Add("Deaths", 0);
        hs.Add("Kills", 0);
        PhotonNetwork.player.SetCustomProperties(hs);
        teamID = temp;
        SpawnMyPlayer();
        matchController = GameObject.Find("Terrain");
    }

    void SpawnMyPlayer()
    {
        if (PhotonNetwork.player.customProperties["teamID"] != null)
        {
            if (spawnSpots == null)
            {
                Debug.Log("No spawnspots");
                PhotonNetwork.LoadLevel("MainMenu");
                return;
            }

            SpawnSpots mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
            while (mySpawnSpot.teamID != teamID)
            {
                mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
            }

            #region turn on player prefabs/scripts
            //Player Settings
            GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Ethan", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
            hasPickedTeam = true;
            myPlayerGO.GetComponent<PhotonView>().RPC("SetTeamID", PhotonTargets.AllBuffered, teamID);
            ((MonoBehaviour)myPlayerGO.GetComponent("TopDownController")).enabled = true;
            ((MonoBehaviour)myPlayerGO.GetComponent("ProjectileShooter")).enabled = true;
            var stats = GameObject.Find("Terrain").GetComponent<tempPlayerStatsStorage>().GetPlayerStats();
            stats.CurrentHealthpoints = 100;
            stats.CurrentChi = 100;
            stats.MaxHealthpoints = 100;
            stats.MaxChi = 100;
            myPlayerGO.GetComponent<PlayerStats>().stats = stats;
            ((MonoBehaviour)myPlayerGO.GetComponent("PlayerStats")).enabled = true;
            ((MonoBehaviour)myPlayerGO.GetComponent("ActionBarController")).enabled = true;
            myPlayerGO.GetComponent<ShowMousePositions>().SetPlayer(myPlayerGO);
            ((MonoBehaviour)myPlayerGO.GetComponent("ShowMousePositions")).enabled = true;

            //Health bar settings
            var healthBar = PhotonNetwork.Instantiate("3DHealthandChiBar", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), 0) as GameObject;
            healthBar.GetComponent<PlayerHealthBarLocation>().SetPlayer(myPlayerGO);
            ((MonoBehaviour)healthBar.GetComponent("PlayerHealthBarLocation")).enabled = true;

            //Camera settings
            var playerCamera = myPlayerGO.transform.FindChild("Main Camera").gameObject;
            ((MonoBehaviour)playerCamera.GetComponent("GameCamera")).enabled = true;

            //MinimapCamera settings
            GameObject miniMapCamera = GameObject.Find("Terrain").transform.FindChild("MiniMapCamera").gameObject;
            deathAdded = false;
            killsAdded = false;
            //var actionBar = myPlayerGO.transform.Find("PlayerGUI").gameObject;
            //((MonoBehaviour)actionBar.GetComponent("ActionBarController")).enabled = true;

            //Healthbar settings
            isDead = false;
            backgroundCamera.SetActive(false);
            //Activate camera
            playerCamera.SetActive(true);
            //Active minimap camera
            miniMapCamera.SetActive(true);
            #endregion
        }
        else
        {
            Debug.Log("The player doesn't have a teamID.");
            
        }
    }
    void OnDisconnectedFromPhoton()
    {
        Debug.Log("Player disconnected");
        Application.LoadLevel("MainMenu");
    }
    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
            if((respawnTimer >1 && respawnTimer < 1.5) && (!deathAdded))
            {
                int numberOfDeaths = (int)PhotonNetwork.player.customProperties["Deaths"];
                numberOfDeaths++;
                ExitGames.Client.Photon.Hashtable hs = new ExitGames.Client.Photon.Hashtable();
                hs.Add("Deaths", numberOfDeaths);
                PhotonNetwork.player.SetCustomProperties(hs);
                deathAdded = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            _escapePressed = !_escapePressed;

        /*
         * 
         * FÖR ATT FÅ ANTAL SPELARE I VARJE LAG
         * 
         * 
        var teamOne = 0;
        var teamTwo = 0;
        foreach(PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.customProperties["teamID"] != null)
            {
                if ((int)player.customProperties["teamID"] == 0)
                    teamOne++;
                else
                    teamTwo++;
            }

        }
        Debug.Log("TeamOne = " + teamOne + ". TeamTwo = " + teamTwo);
         * 
         * */
    }
        

}