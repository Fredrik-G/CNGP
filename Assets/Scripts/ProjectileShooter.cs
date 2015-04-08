using UnityEngine;
using System.Collections;
using System;
using Engine;

public class ProjectileShooter : MonoBehaviour
{

    private GameObject prefab;
    public Transform spawn;

	// Use this for initialization
	void Start ()
	{
		prefab = Resources.Load("projectile") as GameObject;
	}

	// Update is called once per frame
	void Update (){
		if (Input.GetButtonDown("LeftClick"))
	    {
			var projectile = Instantiate(prefab) as GameObject;
	        
			var Controller = projectile.GetComponent<ProjectileSpell>();
			var PlayerStats = gameObject.GetComponent<PlayerStats>();
			Controller.Init(Controller.AdjustActiveSkillValues(PlayerStats.stats.SkillList[0],PlayerStats));
			projectile.transform.localScale = new Vector3((float)Controller.ProjectileActiveSkill.Radius,(float)Controller.ProjectileActiveSkill.Radius,(float)Controller.ProjectileActiveSkill.Radius);

			projectile.transform.position = spawn.position;
	        var rb = projectile.GetComponent<Rigidbody>();
			rb.velocity = spawn.transform.forward*(Convert.ToInt32((Controller.ProjectileActiveSkill.CastSpeed)));

			Destroy(projectile.gameObject, (float)(Controller.ProjectileActiveSkill.Range/Controller.ProjectileActiveSkill.CastSpeed));

	    }
	}
}
