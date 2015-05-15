using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KingOfTheHillController : MonoBehaviour {

	public double teamZeroRedPoints { get; set; }
	public double teamOneBluePoints { get; set; }
    public Image TeamOneBlueImage;
    public Image TeamZeroRedImage;
    private MatchController Match;


	// Use this for initialization
	void Start () {
        teamOneBluePoints = 0;
        teamZeroRedPoints = 0;
        Match = GetComponentInParent<MatchController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        TeamZeroRedImage.fillAmount = (float)(teamZeroRedPoints / 100);
        TeamOneBlueImage.fillAmount = (float)(teamOneBluePoints / 100);
        CheckPoints();
	}

	void OnTriggerStay(Collider other)
	{

		if (other.gameObject.CompareTag ("Player")) {
            if (other.gameObject.GetComponent<PlayerStats>().teamID == 0)
			{
                if (teamOneBluePoints <= 0)
				{
                    if (teamZeroRedPoints < 100)
				    {
                        teamZeroRedPoints = teamZeroRedPoints + 20 * Time.deltaTime;
				    }
				}
				else
				{
                    teamOneBluePoints = teamOneBluePoints - 20 * Time.deltaTime;
				}
			}
			else
			{
                if (teamZeroRedPoints <= 0)
				{
                    if (teamOneBluePoints < 100)
				    {
                        teamOneBluePoints = teamOneBluePoints + 20 * Time.deltaTime;
				    }
				}
				else
				{
                    teamZeroRedPoints = teamZeroRedPoints - 20 * Time.deltaTime;
				}
			}
		}
	}
    void CheckPoints()
    {
        if(teamZeroRedPoints > 0 && teamZeroRedPoints < 20)
        {
            Match.IncreaseRedTeamPointsKOTH(1);
        }
        else if (teamZeroRedPoints > 20 && teamZeroRedPoints < 40)
        {
            Match.IncreaseRedTeamPointsKOTH(2);
        }
        else if (teamZeroRedPoints > 40 && teamZeroRedPoints < 60)
        {
            Match.IncreaseRedTeamPointsKOTH(3);
        }
        else if (teamZeroRedPoints > 60 && teamZeroRedPoints < 80)
        {
            Match.IncreaseRedTeamPointsKOTH(4);
        }
        else if (teamZeroRedPoints > 80 && teamZeroRedPoints < 95)
        {
            Match.IncreaseRedTeamPointsKOTH(5);
        }
        else if (teamZeroRedPoints > 95 && teamZeroRedPoints < 101)
        {
            Match.IncreaseRedTeamPointsKOTH(8);
        }
        else if (teamOneBluePoints > 0 && teamOneBluePoints < 20)
        {
            Match.IncreaseBlueTeamPointsKOTH(1);
        }
        else if (teamOneBluePoints > 20 && teamOneBluePoints < 40)
        {
            Match.IncreaseBlueTeamPointsKOTH(2);
        }
        else if (teamOneBluePoints > 40 && teamOneBluePoints < 60)
        {
            Match.IncreaseBlueTeamPointsKOTH(3);
        }
        else if (teamOneBluePoints > 60 && teamOneBluePoints < 80)
        {
            Match.IncreaseBlueTeamPointsKOTH(4);
        }
        else if (teamOneBluePoints > 80 && teamOneBluePoints < 95)
        {
            Match.IncreaseBlueTeamPointsKOTH(5);
        }
        else if (teamOneBluePoints > 95 && teamOneBluePoints < 101)
        {
            Match.IncreaseBlueTeamPointsKOTH(8);
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
            stream.SendNext(teamOneBluePoints);
            stream.SendNext(teamZeroRedPoints);

        }
        else
        {
            teamOneBluePoints = (double)stream.ReceiveNext();
            teamZeroRedPoints = (double)stream.ReceiveNext();
        }

    }
}
