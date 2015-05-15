using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;
public class GameController : MonoBehaviour
{

    // Use this for initialization
    private bool gameOver = false;
    private MatchController gc;
    private GameObject canvas;
    private bool canvasSpawned = false;
    public bool GetGameOver()
    {
        return gameOver;
    }
    void Start()
    {
        gc = FindObjectOfType<MatchController>();
        canvas = Resources.Load("CanvasTeamWin") as GameObject;
        canvas.SetActive(false);
        //canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if ((gc.PointsRedTeam >= 100 || gc.PointsBlueTeam >= 100) && (!gameOver))
        {
            this.GetComponent<TopDownController>().enabled = false;
            this.GetComponent<ActionBarController>().enabled = false;
            ShowMousePositions smp = this.GetComponent<ShowMousePositions>();
            smp.enabled = false;
            smp.v = 0;
            smp.h = 0;
            this.GetComponent<ProjectileShooter>().enabled = false;
            GameObject kingOfTheHillController = GameObject.Find("Terrain");
            kingOfTheHillController.GetComponentInChildren<KingOfTheHillController>().enabled = false;
            gameOver = true;
            Debug.Log("RED TEAM WINS");
        }
        if (gameOver)
        {
            if (GetComponent<PhotonView>().isMine)
            {
                if (!canvasSpawned)
                {
                    canvas.SetActive(true);
                    Instantiate(canvas);
                    canvasSpawned = true;
                    GameObject teamText = GameObject.Find("TeamText");

                    PhotonNetwork.room.visible = false;
                    PhotonNetwork.room.open = false;
                    teamText.GetComponent<Text>().text = "";

                    if (gc.PointsBlueTeam >= 100)
                    {
                        teamText.GetComponent<Text>().text = "Blue team wins!";
                    }
                    else if (gc.PointsRedTeam >= 100)
                    {
                        teamText.GetComponent<Text>().text = "Red team wins!";
                    }
                    GameObject.Find("MiniMapCamera").SetActive(false);
                    GameObject textPlayerOne = GameObject.Find("TextPlayerOne");
                    GameObject textPlayerTwo = GameObject.Find("TextPlayerTwo");
                    GameObject textPlayerThree = GameObject.Find("TextPlayerThree");
                    GameObject textPlayerFour = GameObject.Find("TextPlayerFour");
                    GameObject textPlayerOneDeaths = GameObject.Find("TextPlayerOneDeaths");
                    GameObject textPlayerTwoDeaths = GameObject.Find("TextPlayerTwoDeaths");
                    GameObject textPlayerThreeDeaths = GameObject.Find("TextPlayerThreeDeaths");
                    GameObject textPlayerFourDeaths = GameObject.Find("TextPlayerFourDeaths");
                    GameObject textPlayerOneKills = GameObject.Find("TextPlayerOneKills");
                    GameObject textPlayerTwoKills = GameObject.Find("TextPlayerTwoKills");
                    GameObject textPlayerThreeKills = GameObject.Find("TextPlayerThreeKills");
                    GameObject textPlayerFourKills = GameObject.Find("TextPlayerFourKills");
                    GameObject textPlayerOneKD = GameObject.Find("TextPlayerOneKD");
                    GameObject textPlayerTwoKD = GameObject.Find("TextPlayerTwoKD");
                    GameObject textPlayerThreeKD = GameObject.Find("TextPlayerThreeKD");
                    GameObject textPlayerFourKD = GameObject.Find("TextPlayerFourKD");
                    var listOfPlayers = PhotonNetwork.playerList;
                    var listOfPlayersLength = listOfPlayers.Length;
                    if (listOfPlayersLength >= 1)
                    {
                        textPlayerOne.GetComponent<Text>().text = "1. " + listOfPlayers[0].ID;
                        textPlayerOneKills.GetComponent<Text>().text = "" + listOfPlayers[0].customProperties["Kills"];
                        textPlayerOneDeaths.GetComponent<Text>().text = "" + listOfPlayers[0].customProperties["Deaths"];
                        if((int)listOfPlayers[0].customProperties["Deaths"]!= 0)
                        {
                            textPlayerOneKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[0].customProperties["Kills"] / (int)listOfPlayers[0].customProperties["Deaths"]);
                        }
                        else if((int)listOfPlayers[0].customProperties["Deaths"] == 0 && (int)listOfPlayers[0].customProperties["Kills"]== 0)
                        {
                            textPlayerOneKD.GetComponent<Text>().text = "0";
                        }
                        else
                        {
                            textPlayerOneKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[0].customProperties["Kills"] /1);
                        }
                    }
                    if (listOfPlayersLength >= 2)
                    {
                        textPlayerTwo.GetComponent<Text>().text = "2. " + listOfPlayers[1].ID + " ";
                        textPlayerTwoKills.GetComponent<Text>().text = "" + listOfPlayers[1].customProperties["Kills"];
                        textPlayerTwoDeaths.GetComponent<Text>().text = "" + listOfPlayers[1].customProperties["Deaths"];
                        if ((int)listOfPlayers[1].customProperties["Deaths"] != 0)
                        {
                            textPlayerTwoKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[1].customProperties["Kills"] / (int)listOfPlayers[1].customProperties["Deaths"]);
                        }
                        else if ((int)listOfPlayers[1].customProperties["Deaths"] == 0 && (int)listOfPlayers[1].customProperties["Kills"] == 0)
                        {
                            textPlayerTwoKD.GetComponent<Text>().text = "0";
                        }
                        else
                        {
                            textPlayerTwoKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[1].customProperties["Kills"] / 1);
                        }
                    }
                    if (listOfPlayersLength >= 3)
                    { 
                        textPlayerThree.GetComponent<Text>().text = "3." + listOfPlayers[2].ID;
                        textPlayerThreeKills.GetComponent<Text>().text = "" + listOfPlayers[2].customProperties["Kills"];
                        textPlayerThreeDeaths.GetComponent<Text>().text = "" +listOfPlayers[2].customProperties["Deaths"];
                        if ((int)listOfPlayers[2].customProperties["Deaths"] != 0)
                        {
                            textPlayerThreeKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[2].customProperties["Kills"] / (int)listOfPlayers[2].customProperties["Deaths"]);
                        }
                        else if ((int)listOfPlayers[2].customProperties["Deaths"] == 0 && (int)listOfPlayers[2].customProperties["Kills"] == 0)
                        {
                            textPlayerThreeKD.GetComponent<Text>().text = "0";
                        }
                        else
                        {
                            textPlayerThreeKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[2].customProperties["Kills"] / 1);
                        }
                    }

                    if (listOfPlayersLength >= 4)
                    {
                        textPlayerFour.GetComponent<Text>().text = "4." + listOfPlayers[3].ID;
                        textPlayerFourKills.GetComponent<Text>().text = "" + listOfPlayers[3].customProperties["Kills"];
                        textPlayerFourDeaths.GetComponent<Text>().text = "" +listOfPlayers[3].customProperties["Deaths"];
                        if ((int)listOfPlayers[3].customProperties["Deaths"] != 0)
                        {
                            textPlayerFourKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[3].customProperties["Kills"] / (int)listOfPlayers[3].customProperties["Deaths"]);
                        }
                        else if ((int)listOfPlayers[3].customProperties["Deaths"] == 0 && (int)listOfPlayers[3].customProperties["Kills"] == 0)
                        {
                            textPlayerFourKD.GetComponent<Text>().text = "0";
                        }
                        else
                        {
                            textPlayerFourKD.GetComponent<Text>().text = "" + (double)((int)listOfPlayers[3].customProperties["Kills"] / 1);
                        }
                    }
                }
                else
                    canvas.SetActive(false);
            }
        }

    }
    void OnGui()
    {

    }
}
