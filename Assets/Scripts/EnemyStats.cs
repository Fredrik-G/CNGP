using UnityEngine;
using System.Collections;
using Engine;

public class EnemyStats : MonoBehaviour {

	public Stats stats = new Stats();

	public GameObject HealthBar;
	public GameObject Frame;
	public float HealthBarMaxScale;
	public float HealthBarCurrentScale;
	
	private double _healthPointPercentage;
	
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
		
		if (other.gameObject.CompareTag ("cylinder"))
		{
			var cylinder = other.gameObject.GetComponent<CylinderSpell>();            
			
			if (cylinder != null)
			{
				TakeDamage(cylinder.CylinderActiveSkill.DamageHealingPower);
				
				for(int i = 0; i < cylinder.CylinderActiveSkill.BuffEffectList.Count; i++)
				{
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Stun > 0)
					{
						StartCoroutine(WaitForStunToEnd(cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Stun));

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
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount > 0)
					{
						GetComponent<EnemyDummyController> ().currentwalkSpeed = (float)(GetComponent<EnemyDummyController> ().currentwalkSpeed * (1 - (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount / 100)));
						Debug.Log("Walk Speed = " + GetComponent<EnemyDummyController> ().currentwalkSpeed);
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("cylinder")) 
		{
			var cylinder = other.gameObject.GetComponent<CylinderSpell>();  
			if (cylinder != null)
			{
				TakeDamage(cylinder.CylinderActiveSkill.DamageHealingPower);
				
				for(int i = 0; i < cylinder.CylinderActiveSkill.BuffEffectList.Count; i++)
				{
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Stun > 0)
					{
						StartCoroutine(WaitForStunToEnd(cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Stun));
						
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
						StartCoroutine(WaitForSlowToEnd(cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.SlowStruct.duration,cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount ));
					}
				}
			}
		}
	}


	IEnumerator WaitForStunToEnd(double duration) {
		GetComponent<EnemyDummyController> ().enabled = false;;
		yield return new WaitForSeconds((float)duration);
		GetComponent<EnemyDummyController> ().enabled = true;
	}

	IEnumerator WaitForSlowToEnd(double duration, double amount) {
		double IncreaseAmountPerHundrethMilliSecond = (GetComponent<EnemyDummyController> ().maxWalkSpeed - GetComponent<EnemyDummyController> ().currentwalkSpeed) / (duration);

		double time = 1;
		Debug.Log("Tid innan" + Time.time);
		while( time <= duration)
		{
			yield return new WaitForSeconds(1);
			GetComponent<EnemyDummyController> ().currentwalkSpeed += (float)IncreaseAmountPerHundrethMilliSecond;
			time++;
		}

		Debug.Log("Tid efter" + Time.time);

	}


}
