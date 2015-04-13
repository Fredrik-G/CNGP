using UnityEngine;
using System.Collections;
using System;
using Engine;

public class ProjectileShooter : MonoBehaviour
{

    private GameObject _projectilePrefab;
	private GameObject _cubePrefab;
	private GameObject _cylinderPrefab;
	public Transform spawn;

	// Use this for initialization
	void Start ()
	{
		_projectilePrefab = Resources.Load("projectile") as GameObject;
		_cubePrefab = Resources.Load ("cube") as GameObject;
		_cylinderPrefab = Resources.Load ("cylinder") as GameObject;

	}

	// Update is called once per frame
	void Update (){
		var PlayerStats = gameObject.GetComponent<PlayerStats>();
	
		if (Input.GetButton("LeftClick") && (PlayerStats.stats.SkillList[0].CurrentCooldown <= 0))
	    {
			SkillOnMouseCast(0, PlayerStats);
	    }

		if (Input.GetButton("RightClick") && (PlayerStats.stats.SkillList[1].CurrentCooldown <= 0))
		{
			SkillCast(1,PlayerStats);	

		}

		if (Input.GetButton ("ButtonQ") && (PlayerStats.stats.SkillList [2].CurrentCooldown <= 0))
		{
			SkillCast(2,PlayerStats);
		}

		if (Input.GetButton ("ButtonE") && (PlayerStats.stats.SkillList [3].CurrentCooldown <= 0))
		{
			SkillCast(3,PlayerStats);
		}


		for (int i = 0; i < PlayerStats.stats.SkillList.Count; i++) {
			PlayerStats.stats.SkillList [i].CurrentCooldown -= Time.deltaTime;
		}
	}
	
	void SkillCast(int skillSlot, PlayerStats playerStats)
	{
		var oldSpawn = spawn.localRotation;
		if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
		{
			spawn.transform.Rotate (Vector3.up, -15);
		}

		for (int i = 0; i < playerStats.stats.SkillList[skillSlot].NumberOfProjectiles; i++) 
		{
			var projectile = Instantiate (_projectilePrefab) as GameObject;
		
			var Controller = projectile.GetComponent<ProjectileSpell> ();

			Controller.Init (Controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));
			playerStats.stats.SkillList [skillSlot].CurrentCooldown = Controller.ProjectileActiveSkill.Cooldown;
			Controller.Name = playerStats.stats.SkillList [skillSlot].Name;
			Controller.Scale = playerStats.stats.SkillList [skillSlot].Radius;
			projectile.transform.localScale = new Vector3 ((float)Controller.ProjectileActiveSkill.Radius, (float)Controller.ProjectileActiveSkill.Radius, (float)Controller.ProjectileActiveSkill.Radius);
		
			if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
			{
				spawn.transform.Rotate (Vector3.up, 5);
			}
			projectile.transform.position = spawn.position;

			var rb = projectile.GetComponent<Rigidbody> ();
			rb.velocity = spawn.transform.forward * (Convert.ToInt32 ((Controller.ProjectileActiveSkill.CastSpeed)));
		
			Destroy (projectile.gameObject, (float)(Controller.ProjectileActiveSkill.Range / Controller.ProjectileActiveSkill.CastSpeed));
		}

		spawn.localRotation = oldSpawn;
	}

	void SkillOnMouseCast(int skillSlot, PlayerStats playerStats)
	{
		var cylinder = Instantiate (_cylinderPrefab) as GameObject;

		var Controller = cylinder.GetComponent<CylinderSpell> ();
		Controller.Init (Controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));

		// FÅ IN RÄTT MUSKOORDINATER
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast(ray, out hit,Mathf.Infinity)){
			
			if(hit.collider.tag == "Terrain"){
				var placePos = hit.point;
				placePos.y += (float)0.5;
				placePos.x = Mathf.Round(placePos.x);
				placePos.z = Mathf.Round(placePos.z);
				Debug.Log("World point: " + placePos);
			}
		}
		Debug.Log("World point: " + hit.point);
		//cylinder.transform.position = new Vector3 (point.x, point.y, point.z);

		var rb = cylinder.GetComponent<Rigidbody> ();
		rb.velocity = cylinder.transform.up * 2;
		Destroy (cylinder.gameObject, 10);
	}
}
