using UnityEngine;
using System.Collections;
using Engine;

public class EnemyDummyController : MonoBehaviour {

	public Stats stats = new Stats();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void TakeDamage(double amount)
	{
		stats.Healthpoints -= (amount / (stats.Armor / 100));
		
		if (stats.Healthpoints <= 0)
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
	}
}
