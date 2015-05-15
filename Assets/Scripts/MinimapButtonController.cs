using UnityEngine;
using System.Collections;

public class MinimapButtonController : MonoBehaviour {

    public int teamId;
    private PlayerStats playerStats;
    public GameObject Sphere; 
	// Use this for initialization
	void Start () {
        playerStats = GetComponentInParent<PlayerStats>();

        teamId = playerStats.teamID;
	}
	
	// Update is called once per frame
	void Update () {
        teamId = playerStats.teamID;
	    if(teamId == 0)
        {
            Sphere.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            Sphere.GetComponent<Renderer>().material.color = Color.blue;
        }
	}
}
