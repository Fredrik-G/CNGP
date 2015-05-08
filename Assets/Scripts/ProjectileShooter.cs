using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using Engine;

public class ProjectileShooter : MonoBehaviour
{

    private GameObject _projectilePrefab;
	private GameObject _cubePrefab;
	private GameObject _cylinderPrefab;
	public Transform Spawn;
	private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
	{
		_projectilePrefab = Resources.Load("projectile") as GameObject;
		_cubePrefab = Resources.Load ("cube") as GameObject;
		_cylinderPrefab = Resources.Load ("cylinder") as GameObject;
		_audioSource = GetComponent <AudioSource> () as AudioSource;

	}

	// Update is called once per frame
	void Update (){
		var playerStats = gameObject.GetComponent<PlayerStats>();
	
		if (Input.GetButton("LeftClick") && (playerStats.stats.SkillList[0].CurrentCooldown <= 0))
	    {
            var arguments = new object[2];
            arguments[0] = 0;
            arguments[1] = playerStats;
            var skillName = "Skill" + playerStats.stats.SkillList[0].Name;

	        StartCoroutine(skillName, arguments);

	    }

		if (Input.GetButton("RightClick") && (playerStats.stats.SkillList[1].CurrentCooldown <= 0))
		{
            SkillOnMouseCast(1, playerStats);	

		}

		if (Input.GetButton ("ButtonQ") && (playerStats.stats.SkillList [2].CurrentCooldown <= 0))
		{
			SkillCast(2,playerStats);
		}

		if (Input.GetButton ("ButtonE") && (playerStats.stats.SkillList [3].CurrentCooldown <= 0))
		{
			SkillCast(3,playerStats);
		}

		for (int i = 0; i < playerStats.stats.SkillList.Count; i++) {

		    if (playerStats.stats.SkillList[i].CurrentCooldown > 0)
		    {
		        playerStats.stats.SkillList[i].CurrentCooldown -= Time.deltaTime;
		    }
		}
	}

	//Anpassad för de fyra mainskillsen
	void SkillCast(int skillSlot, PlayerStats playerStats)
	{
		_audioSource.PlayOneShot (Resources.Load ("SoundEffects/" + playerStats.stats.SkillList [skillSlot].Name) as AudioClip);
		var oldSpawn = Spawn.localRotation;
		if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
		{
			Spawn.transform.Rotate (Vector3.up, -15);
		}

		for (int i = 0; i < playerStats.stats.SkillList[skillSlot].NumberOfProjectiles; i++) 
		{	
			var projectile = Instantiate (_projectilePrefab, Spawn.position, Quaternion.Euler(0, 0, 0)) as GameObject;

			var controller = projectile.GetComponent<ProjectileSpell> ();

			controller.Init (controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));
			playerStats.stats.SkillList [skillSlot].CurrentCooldown = controller.ProjectileActiveSkill.Cooldown;
			controller.Scale = controller.ProjectileActiveSkill.Radius;
		    controller.Class = playerStats.Class;

			projectile.transform.localScale = new Vector3 ((float)controller.Scale , (float)controller.Scale , (float)controller.Scale);
		
			if (playerStats.stats.SkillList [skillSlot].Name == "Waterbullets")
			{
				Spawn.transform.Rotate (Vector3.up, 5);
			}

			var rb = projectile.GetComponent<Rigidbody> ();
			rb.velocity = Spawn.transform.forward * (Convert.ToInt32 ((controller.ProjectileActiveSkill.CastSpeed)));
		
			Destroy (projectile.gameObject, (float)(controller.ProjectileActiveSkill.Range / controller.ProjectileActiveSkill.CastSpeed));
		}

		Spawn.localRotation = oldSpawn;
	}


	//Anpassad för skillen EarthQuake och AirVortex
	void SkillOnMouseCast(int skillSlot, PlayerStats playerStats)
	{
		if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList [skillSlot].ChiCost) {
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, (float)playerStats.stats.SkillList [skillSlot].Range)) {
				_audioSource.PlayOneShot (Resources.Load ("SoundEffects/" + playerStats.stats.SkillList [skillSlot].Name) as AudioClip);
				var cylinderPosition = new Vector3 (hit.point.x, 0, hit.point.z);


				var cylinder = (GameObject)Instantiate (_cylinderPrefab, cylinderPosition, Quaternion.Euler (0, 0, 0));


				var controller = cylinder.gameObject.GetComponent<CylinderSpell> ();
                controller.Class = playerStats.Class;
                

				controller.Init (controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));
				cylinder.transform.localScale = new Vector3 ((float)controller.CylinderActiveSkill.Radius , cylinder.transform.localScale.y, (float)controller.CylinderActiveSkill.Radius);
				playerStats.stats.SkillList [skillSlot].CurrentCooldown = controller.CylinderActiveSkill.Cooldown;

				var rb = cylinder.gameObject.GetComponent<Rigidbody> ();
				if(controller.CylinderActiveSkill.Name.Equals("Earthquake"))
				{
					rb.velocity = cylinder.transform.up * 2;
				}

				if(controller.CylinderActiveSkill.Name.Equals("AirVortex"))
				{
					rb.useGravity = false;
					cylinder.transform.localScale = new Vector3 ((float)controller.CylinderActiveSkill.Radius , 5, (float)controller.CylinderActiveSkill.Radius);
				}
				else
				{
					cylinder.transform.localScale = new Vector3 ((float)controller.CylinderActiveSkill.Radius , cylinder.transform.localScale.y, (float)controller.CylinderActiveSkill.Radius);
				}


				playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

				if(controller.CylinderActiveSkill.Name.Equals("Earthquake"))
				{
					Destroy (cylinder.gameObject, 2);
				}
				
				if(controller.CylinderActiveSkill.Name.Equals("AirVortex"))
				{
					Destroy (cylinder.gameObject, 5);
				}

			}
		}
	}
	
	//Anpassad för skillen Ice Floor och Blazing Ring
	void SkillSpawnOnPlayerCast(int skillSlot, PlayerStats playerStats)
	{
		if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList [skillSlot].ChiCost) 
		{
			_audioSource.PlayOneShot (Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);
			var cylinderPosition = new Vector3(gameObject.transform.position.x, (float)(gameObject.transform.position.y ), gameObject.transform.position.z);
			var cylinder = Instantiate (_cylinderPrefab, cylinderPosition , Quaternion.Euler (0, 0, 0)) as GameObject;

			var controller = cylinder.GetComponent<CylinderSpell> ();

			controller.Init (controller.AdjustActiveSkillValues (playerStats.stats.SkillList [skillSlot], playerStats));

			playerStats.stats.SkillList [skillSlot].CurrentCooldown = controller.CylinderActiveSkill.Cooldown;

			var rb = cylinder.GetComponent<Rigidbody> ();
			rb.useGravity = false;

			playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

			Destroy(cylinder.gameObject, (float)0.5);
		}
	}


    void SkillIceRamp(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost*Time.deltaTime;

        }
    }

    void SkillRockShoes(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;
        }
    }

    void SkillEnhancedSpeed(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;


            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
            if (Physics.Raycast(ray, out hit, 1000))
            {
                var mousePos = new Vector3(hit.point.x, 0, hit.point.z);       
                transform.position = Vector3.MoveTowards(transform.position, mousePos, (float)(playerStats.stats.SkillList[skillSlot].Range * (playerStats.stats.Skillrange / 100) *  playerStats.stats.SkillrangeFactor));
            }


        }
    }

    void SkillJetPropulsion(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= (playerStats.stats.SkillList[skillSlot].ChiCost*Time.deltaTime);

        }
    }

    void SkillWaterShield(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

        }
    }

    void SkillAirShield(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);

            if (playerStats.stats.CurrentHealthpoints +
                    (playerStats.stats.SkillList[skillSlot].DamageHealingPower * (playerStats.stats.Healingpower / 100) *
                     playerStats.stats.HealingPowerFactor) >= playerStats.stats.MaxHealthpoints)
            {
                playerStats.stats.CurrentHealthpoints = playerStats.stats.MaxHealthpoints;
            }

            else
            {
                playerStats.stats.CurrentHealthpoints +=
                    (playerStats.stats.SkillList[skillSlot].DamageHealingPower * (playerStats.stats.Healingpower / 100) *
                     playerStats.stats.HealingPowerFactor);
            }

            var cylinderPosition = new Vector3(gameObject.transform.position.x, (float)(gameObject.transform.position.y), gameObject.transform.position.z);
            var cylinder = Instantiate(_cylinderPrefab, cylinderPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

            var controller = cylinder.GetComponent<CylinderSpell>();
            controller.Class = playerStats.Class;
            var rb = cylinder.GetComponent<Rigidbody>();
            rb.useGravity = false;

            controller.Init(controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));

            cylinder.transform.localScale = new Vector3((float)controller.CylinderActiveSkill.Radius, 1, (float)controller.CylinderActiveSkill.Radius);

            

            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

            Destroy(cylinder.gameObject, 1);

        }
    }

    void SkillEarthArmor(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

        }
    }

    void SkillShieldOfFire(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            foreach (var item in playerStats.stats.SkillList[skillSlot].BuffEffectList)
            {
                var foundEffect = GetComponent<PlayerStats>().EffectList.Find(x => x.Effect.Skillname == item.Effect.Skillname);

                if (foundEffect == null)
                {
                    GetComponent<PlayerStats>()
                        .EffectList.Add(new BuffEffect(item.Info,
                            new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < item.Effect.Timeleft)
                    {
                        GetComponent<PlayerStats>().EffectList.Remove(foundEffect);
                        GetComponent<PlayerStats>()
                            .EffectList.Add(new BuffEffect(item.Info,
                                new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft,
                                    item.Effect.Duration * playerStats.stats.Buffeffectduration, item.Effect.Amount)));
                    }
                }
            }

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

        }
    }

    void SkillBloodbending(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;
        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, (float)playerStats.stats.SkillList[skillSlot].Range))
            {
                playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
                _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);
                var cylinderPosition = new Vector3(hit.point.x, 0, hit.point.z);


                var cylinder = (GameObject)Instantiate(_cylinderPrefab, cylinderPosition, Quaternion.Euler(0, 0, 0));


                var controller = cylinder.gameObject.GetComponent<CylinderSpell>();
                controller.Class = playerStats.Class;


                controller.Init(controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));
                cylinder.transform.localScale = new Vector3((float)controller.CylinderActiveSkill.Radius, cylinder.transform.localScale.y, (float)controller.CylinderActiveSkill.Radius);

                var rb = cylinder.gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;

                playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

                Destroy(cylinder.gameObject, (float)0.5);

            }
        }
    }

    void SkillEarthBomb(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;

        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            var projectile = Instantiate(_projectilePrefab, Spawn.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            var controller = projectile.GetComponent<ProjectileSpell>();

            controller.Init(controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));
            controller.Scale = controller.ProjectileActiveSkill.Radius;
            controller.Class = playerStats.Class;

            projectile.transform.localScale = new Vector3((float) controller.Scale, (float) controller.Scale,
                (float) controller.Scale);

            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Spawn.transform.forward*(Convert.ToInt32((controller.ProjectileActiveSkill.CastSpeed)));

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

            Destroy(projectile.gameObject,
                (float) (controller.ProjectileActiveSkill.Range/controller.ProjectileActiveSkill.CastSpeed));
        }
    }

    void SkillStrongWind(object[] arguments)
    {
        var skillSlot = (int)arguments[0];
        var playerStats = arguments[1] as PlayerStats;

        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = playerStats.stats.SkillList[skillSlot].Cooldown;
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/" + playerStats.stats.SkillList[skillSlot].Name) as AudioClip);


            var projectile = Instantiate(_projectilePrefab, Spawn.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            var controller = projectile.GetComponent<ProjectileSpell>();

            controller.Init(controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));
            controller.Scale = controller.ProjectileActiveSkill.Radius;
            controller.Class = playerStats.Class;

            projectile.transform.localScale = new Vector3((float)controller.Scale, (float)controller.Scale,
                (float)controller.Scale);

            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Spawn.transform.forward * (Convert.ToInt32((controller.ProjectileActiveSkill.CastSpeed)));

            playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;

            Destroy(projectile.gameObject,
                (float)(controller.ProjectileActiveSkill.Range / controller.ProjectileActiveSkill.CastSpeed));
        }
    }
}
