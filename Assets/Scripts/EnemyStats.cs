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
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Dot > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Hot > 0)
					{
						
					}
					if (cylinder.CylinderActiveSkill.BuffEffectList[i].Effect.Slow > 0)
					{
						
					}
				}
			}
		}
	}

	IEnumerator WaitForStunToEnd(double duration) {
		GetComponent<EnemyDummyController> ().enabled = false;
		Debug.Log("Stunned");
		yield return new WaitForSeconds((float)duration);
		GetComponent<EnemyDummyController> ().enabled = true;
		Debug.Log("Not-Stunned");
	}

}
