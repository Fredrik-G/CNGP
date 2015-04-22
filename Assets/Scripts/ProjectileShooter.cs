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

	//Anpassad för de fyra mainskillsen
	void SkillCast(int skillSlot, PlayerStats playerStats)
	{
		var oldSpawn = spawn.localRotation;
		if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
		{
			spawn.transform.Rotate (Vector3.up, -15);
		}

		for (int i = 0; i < playerStats.stats.SkillList[skillSlot].NumberOfProjectiles; i++) 
		{	
			var projectile = Instantiate (_projectilePrefab, spawn.position, Quaternion.Euler(0, 0, 0)) as GameObject;

			var Controller = projectile.GetComponent<ProjectileSpell> ();

			Controller.Init (Controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));
			playerStats.stats.SkillList [skillSlot].CurrentCooldown = Controller.ProjectileActiveSkill.Cooldown;
			Controller.Name = playerStats.stats.SkillList [skillSlot].Name;
			Controller.Scale = Controller.ProjectileActiveSkill.Radius;

			projectile.transform.localScale = new Vector3 ((float)Controller.Scale , (float)Controller.Scale , (float)Controller.Scale);
		
			if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
			{
				spawn.transform.Rotate (Vector3.up, 5);
			}

			var rb = projectile.GetComponent<Rigidbody> ();
			rb.velocity = spawn.transform.forward * (Convert.ToInt32 ((Controller.ProjectileActiveSkill.CastSpeed)));
		
			Destroy (projectile.gameObject, (float)(Controller.ProjectileActiveSkill.Range / Controller.ProjectileActiveSkill.CastSpeed));
		}

		spawn.localRotation = oldSpawn;
	}


	//Anpassad för skillen EarthQuake
	void SkillOnMouseCast(int skillSlot, PlayerStats playerStats)
	{
		if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList [skillSlot].ChiCost) {
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, (float)playerStats.stats.SkillList [skillSlot].Range)) {
				var cylinderPosition = new Vector3 (hit.point.x, 0, hit.point.z);


				var cylinder = Instantiate (_cylinderPrefab, cylinderPosition, Quaternion.Euler (0, 0, 0)) as GameObject;


				var Controller = cylinder.GetComponent<CylinderSpell> ();
				Controller.Init (Controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));
				cylinder.transform.localScale = new Vector3 ((float)Controller.CylinderActiveSkill.Radius , cylinder.transform.localScale.y, (float)Controller.CylinderActiveSkill.Radius);

				playerStats.stats.SkillList [skillSlot].CurrentCooldown = Controller.CylinderActiveSkill.Cooldown;

				var rb = cylinder.GetComponent<Rigidbody> ();
				if(Controller.CylinderActiveSkill.Name.Equals("Earthquake"))
				{
					rb.velocity = cylinder.transform.up * 2;
				}

				if(Controller.CylinderActiveSkill.Name.Equals("Air vortex"))
				{
					rb.useGravity = false;
					cylinder.transform.localScale = new Vector3 ((float)Controller.CylinderActiveSkill.Radius , 5, (float)Controller.CylinderActiveSkill.Radius);
				}
				else
				{
					cylinder.transform.localScale = new Vector3 ((float)Controller.CylinderActiveSkill.Radius , cylinder.transform.localScale.y, (float)Controller.CylinderActiveSkill.Radius);
				}


				playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

				Debug.Log("" + playerStats.stats.CurrentChi);

				Destroy (cylinder.gameObject, 2);
			}
		}
	}
<<<<<<< HEAD

	//Anpassad för skillen Ice Floor och Blazing Ring
	void SkillSpawnOnPlayerCast(int skillSlot, PlayerStats playerStats)
	{
		if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList [skillSlot].ChiCost) 
		{
			_audioSource.PlayOneShot (Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);
			var cylinderPosition = new Vector3(gameObject.transform.position.x, (float)(gameObject.transform.position.y ), gameObject.transform.position.z);
			var cylinder = Instantiate (_cylinderPrefab, cylinderPosition , Quaternion.Euler (0, 0, 0)) as GameObject;

			var Controller = cylinder.GetComponent<CylinderSpell> ();
			Controller.Init (Controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));

			playerStats.stats.SkillList [skillSlot].CurrentCooldown = Controller.CylinderActiveSkill.Cooldown;

			var rb = cylinder.GetComponent<Rigidbody> ();
			rb.useGravity = false;

			playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

			Destroy(cylinder.gameObject, (float)0.5);
		}
	}
=======
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
}
