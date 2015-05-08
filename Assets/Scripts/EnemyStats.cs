using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Engine;

public class EnemyStats : MonoBehaviour {

	public Stats stats = new Stats();
	public List<BuffEffect> EffectList = new List<BuffEffect>();

	public GameObject HealthBar;
	public GameObject Frame;
	public float HealthBarMaxScale;
	public float HealthBarCurrentScale;
	
	private double _healthPointPercentage;
	private double _airVortexTimer = 0;

    public bool Stunned = false;
    public bool Rooted = false;
	
	// Use this for initialization
	void Start () {
		stats.CurrentHealthpoints = 100;
		stats.MaxHealthpoints = 100;
		HealthBarMaxScale = HealthBar.transform.localScale.y;
		HealthBarCurrentScale = HealthBarMaxScale;
	}
	
	// Update is called once per frame
	void Update () {

		if (stats.CurrentHealthpoints <= stats.MaxHealthpoints) {
			stats.CurrentHealthpoints += ((stats.Healthreg / 100) * Time.deltaTime);
			_healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
			HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale); 
			HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
		}

        if (stats.CurrentHealthpoints <= 0)
        {
            Destroy(gameObject);
        }
	}

	void FixedUpdate()
	{
		SetFactorValues ();
	}

	private void SetFactorValues()
	{
		stats.MovementSpeedFactor = 1;
	    Stunned = false;
	    Rooted = false;
		for(int i = 0; i < EffectList.Count; i++) 
		{
            if (EffectList[i].Effect.Type.Equals("Slow") || EffectList[i].Effect.Type.Equals("Tapering slow"))
			{
				stats.MovementSpeedFactor = stats.MovementSpeedFactor * (1 - (EffectList[i].Effect.Amount / 100));
			}

			if (EffectList[i].Effect.Type.Equals("Stun"))
			{
			    Stunned = true;
			}

		    if (EffectList[i].Effect.Type.Equals("Root"))
		    {
		        Rooted = true;
		    }

            //if för alla effekttyper..
			EffectList[i].Effect.Timeleft -= Time.deltaTime;
			

			if(EffectList[i].Effect.Timeleft <= 0)
			{
				EffectList.RemoveAt(i);
			}

		}


	}

	private void TakeDamage(double amount, string Class)
	{
	    if (Class.Equals("Fire") || Class.Equals("Water"))
	    {
            stats.CurrentHealthpoints -= (amount / ((stats.Armor / 100) * stats.ArmorFactor) / ((stats.Magicalresistance / 100) * stats.MagicalresistanceFactor));
            _healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
            HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale);
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
	    }
	    else
	    {
            stats.CurrentHealthpoints -= (amount / ((stats.Armor / 100) * stats.ArmorFactor) / ((stats.Physicalresistance / 100) * stats.PhysicalresistanceFactor));
            _healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
            HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale);
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
	        
	    }
		
		if (stats.CurrentHealthpoints <= 0)
		{
			Destroy(gameObject);
		}
	}

    public IEnumerator TakeDamageOverTime(double amount, double duration, string Class)
    {
        var damagePerHundrethMilliSecond = amount/duration/10;
        double time = 0;

        while (time <= duration)
        {
            
            TakeDamage(damagePerHundrethMilliSecond, Class);
            yield return new WaitForSeconds((float)0.1);

            time += 0.1;
        }
    }

    public void Heal(double amount)
    {
        if (stats.CurrentHealthpoints + amount >= stats.MaxHealthpoints)
        {
            stats.CurrentHealthpoints = stats.MaxHealthpoints;
            _healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
            HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale);
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
        }

        else
        {
            stats.CurrentHealthpoints += amount;
            _healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
            HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale);
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            var projectile = other.gameObject.GetComponent<ProjectileSpell>();

            if (projectile != null)
            {
                TakeDamage(projectile.ProjectileActiveSkill.DamageHealingPower, projectile.Class);

                for (int i = 0; i < projectile.ProjectileActiveSkill.BuffEffectList.Count; i++)
                {
                    var foundEffect = EffectList.Find(x => x.Effect.Skillname == projectile.ProjectileActiveSkill.BuffEffectList[i].Effect.Skillname);
                    if (foundEffect == null)
                    {
                        EffectList.Add(projectile.ProjectileActiveSkill.BuffEffectList[i]);
                    }
                    else
                    {
                        if (foundEffect.Effect.Timeleft < projectile.ProjectileActiveSkill.BuffEffectList[i].Effect.Timeleft)
                        {
                            EffectList.Remove(foundEffect);
                            EffectList.Add(projectile.ProjectileActiveSkill.BuffEffectList[i]);
                        }
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("cylinder"))
        {
            var cylinder = other.gameObject.GetComponent<CylinderSpell>();
            
            if ((cylinder != null) && !(cylinder.CylinderActiveSkill.Name.Equals("AirShield")))
            {
                
                if(!(cylinder.CylinderActiveSkill.Name.Equals("Bloodbending")))
                {
                    TakeDamage(cylinder.CylinderActiveSkill.DamageHealingPower, cylinder.Class);
                }
                

                for (int i = 0; i < cylinder.CylinderActiveSkill.BuffEffectList.Count; i++)
                {
                    var foundEffect = EffectList.Find(x => x.Effect.Skillname == cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Skillname);
                    if (foundEffect == null)
                    {
                        EffectList.Add(cylinder.CylinderActiveSkill.BuffEffectList[i]);
                    }
                    else
                    {
                        if (foundEffect.Effect.Timeleft < cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Timeleft)
                        {
                            EffectList.Remove(foundEffect);
                            EffectList.Add(cylinder.CylinderActiveSkill.BuffEffectList[i]);
                        }
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("cylinder"))
        {
            var cylinder = other.gameObject.GetComponent<CylinderSpell>();
            if (cylinder.CylinderActiveSkill.Name.Equals("AirVortex"))
            {
                for (int i = 0; i < cylinder.CylinderActiveSkill.BuffEffectList.Count; i++)
                {
                    var foundEffect = EffectList.Find(x => x.Effect.Skillname == cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Skillname);
                    if (foundEffect == null)
                    {
                        EffectList.Add(cylinder.CylinderActiveSkill.BuffEffectList[i]);
                    }
                    else
                    {
                        if (foundEffect.Effect.Timeleft < cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Timeleft)
                        {
                            EffectList.Remove(foundEffect);
                            EffectList.Add(cylinder.CylinderActiveSkill.BuffEffectList[i]);
                        }
                    }
                }

                if (_airVortexTimer > 0.1)
                {
                    TakeDamage(((other.gameObject.GetComponent<CylinderSpell>().CylinderActiveSkill.DamageHealingPower) / 10), cylinder.Class);

                    _airVortexTimer = 0;
                }

                _airVortexTimer += Time.deltaTime;

            }
        }
    }
}
