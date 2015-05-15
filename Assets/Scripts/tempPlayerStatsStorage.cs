using UnityEngine;
using System.Collections;
using Engine;
public class tempPlayerStatsStorage : MonoBehaviour {
    private Stats playerStats = new Stats();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetPlayerStats(Stats ps)
    {
        this.playerStats = ps;
    }
    public Stats GetPlayerStats()
    {
        return playerStats;
    }
}