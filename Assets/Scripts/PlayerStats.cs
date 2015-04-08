using UnityEngine;
using System.Collections;
using Engine;

public class PlayerStats : MonoBehaviour {

	public Stats stats = new Stats();
	// Use this for initialization
	void Start () {
		stats.SkillList.Add(new ActiveSkill ("Earthblock", "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.3, false, false, true, false, 0.5, 15, 0, 11));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
