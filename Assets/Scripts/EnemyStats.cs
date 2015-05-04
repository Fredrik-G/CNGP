using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	
	// Use this for initialization
	void Start () {
		stats.CurrentHealthpoints = 20;
		stats.MaxHealthpoints = 20;
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
		//GetComponent<EnemyDummyController> ().enabled = true;
		for(int i = 0; i < EffectList.Count; i++) 
		{
			if(EffectList[i].Effect.Type.Equals("Slow"))
			{
				Debug.Log("" + EffectList[i].Effect.Timeleft );
				stats.MovementSpeedFactor = stats.MovementSpeedFactor * (1 - (EffectList[i].Effect.Amount / 100));
			}

			if(EffectList[i].Effect.Type.Equals("Stun"))
			{
				GetComponent<EnemyDummyController> ().enabled = false;
				Debug.Log("" + EffectList[i].Effect.Timeleft );
			}

            //if för alla effekttyper..
			EffectList[i].Effect.Timeleft -= Time.deltaTime;
			

			if(EffectList[i].Effect.Timeleft <= 0)
			{
				EffectList.RemoveAt(i);
			}

		}


	}

	private void TakeDamage(double amount)
	{
		stats.CurrentHealthpoints -= (amount / (stats.Armor / 100));
		_healthPointPercentage = (stats.CurrentHealthpoints / stats.MaxHealthpoints);
		HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale); 
		HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
		
		
		if (stats.CurrentHealthpoints <= 0)
		{
			Destroy(gameObject);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            var projectile = other.gameObject.GetComponent<ProjectileSpell>();

            if (projectile != null)
            {
                TakeDamage(projectile.ProjectileActiveSkill.DamageHealingPower);
            }
        }

        if (other.gameObject.CompareTag("cylinder"))
        {
            var cylinder = other.gameObject.GetComponent<CylinderSpell>();

            if (cylinder != null)
            {

                TakeDamage(cylinder.CylinderActiveSkill.DamageHealingPower);

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
            if (cylinder.CylinderActiveSkill.Name.Equals("Air vortex"))
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
                    TakeDamage((other.gameObject.GetComponent<CylinderSpell>().CylinderActiveSkill.DamageHealingPower) / 10);

                    _airVortexTimer = 0;
                }

                _airVortexTimer += Time.deltaTime;

            }
        }
    }

	/*
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("cylinder")) 
		{

			var cylinder = other.gameObject.GetComponent<CylinderSpell>();  
			if (cylinder != null)
			{
				for(int i = 0; i < cylinder.CylinderActiveSkill.BuffEffectList.Count; i++)
				{
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Stun > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Root > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Silence > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.DotStruct.dotamount > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.HotStruct.hotamount > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount> 0)
					{
						GetComponent<EnemyDummyController> ().currentWalkSpeed = (float)GetComponent<EnemyDummyController> ().maxWalkSpeed;
					}
				}
			}
		}
	}
*/

	IEnumerator WaitForStunToEnd(double duration) {
		GetComponent<EnemyDummyController> ().enabled = false;;
		yield return new WaitForSeconds((float)duration);
		GetComponent<EnemyDummyController> ().enabled = true;
	}

	/*
	IEnumerator WaitForSlowToEnd(double duration, double amount) {
		double IncreaseAmountPerHundrethMilliSecond = (GetComponent<EnemyDummyController> ().maxWalkSpeed - GetComponent<EnemyDummyController> ().currentWalkSpeed) / (duration);

		double time = 1;
		Debug.Log("Tid innan" + Time.time);
		while( time <= duration)
		{
			yield return new WaitForSeconds(1);
			GetComponent<EnemyDummyController> ().currentWalkSpeed += (float)IncreaseAmountPerHundrethMilliSecond;
			time++;
		}

		Debug.Log("Tid efter" + Time.time);

	}
	*/


}
