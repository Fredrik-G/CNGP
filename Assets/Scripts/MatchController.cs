using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour
{

    public double PointsRedTeam { get; set; }
    public double PointsBlueTeam { get; set; }
    public double CabbageManTimer { get; set; }

    private MapGuiController mgc;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        PointsRedTeam = 90;
        PointsBlueTeam = 90;
        CabbageManTimer = 5;

        // var CabbageMan = Instantiate (Resources.Load ("ObjectiveCabbage")) as GameObject;
        // CabbageMan.transform.parent = GameObject.Find ("Terrain").transform;
    }
    [RPC]
    public void SetPoints(double pointsRedTeam, double pointsBlueTeam)
    {
        this.PointsRedTeam = pointsRedTeam;
        this.PointsBlueTeam = pointsBlueTeam;
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            CheckCabbageMan();
            
        }
       
    }


    [RPC]
    public void IncreaseRedTeamPointsOnKill()
    {

        if (PointsRedTeam <= 100)
        {
            PointsRedTeam = PointsRedTeam + 5;
        }

    }
    [RPC]
    public void IncreaseBlueTeamPointsOnKill()
    {

        if (PointsBlueTeam <= 100)
        {
            PointsBlueTeam = PointsBlueTeam + 5;
        }
    }
    [RPC]
    public void IncreasePointsCabbageKill(int team)
    {

        if (team == 0)
        {
            PointsRedTeam = PointsRedTeam + 5;
        }
        else
        {
            PointsBlueTeam = PointsBlueTeam + 5;
        }

    }
    [RPC]
    public void IncreaseRedTeamPointsKOTH(double amount)
    {

        if (PointsRedTeam <= 100)
        {
            PointsRedTeam = PointsRedTeam + amount * Time.deltaTime;
        }

    }
    [RPC]
    public void IncreaseBlueTeamPointsKOTH(double amount)
    {

        if (PointsBlueTeam <= 100)
        {
            PointsBlueTeam = PointsBlueTeam + amount * Time.deltaTime;
        }

    }

    void CheckWinner()
    {
        if (PointsRedTeam >= 100)
        {
            //do something
        }
        if (PointsBlueTeam >= 100)
        {
            //do something
        }
    }
    void CheckCabbageMan()
    {

        if (CabbageManTimer < 0)
        {
            var CabbageMan = (GameObject)PhotonNetwork.Instantiate("ObjectiveCabbage", new Vector3(55, 0, 21), new Quaternion(0, 0, 0, 0), 0);
            CabbageMan.transform.parent = GameObject.Find("Terrain").transform;
            CabbageManTimer = 100;
        }
        else if (CabbageManTimer <= 10)
        {
            CabbageManTimer -= Time.deltaTime;
        }


    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(PointsRedTeam);
            stream.SendNext(PointsBlueTeam);
            stream.SendNext(CabbageManTimer);

        }
        else
        {
            PointsRedTeam = (double)stream.ReceiveNext();
            PointsBlueTeam = (double)stream.ReceiveNext();
            CabbageManTimer = (double)stream.ReceiveNext();
        }

    }
}
