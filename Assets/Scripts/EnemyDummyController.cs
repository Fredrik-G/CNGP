using UnityEngine;
using System.Collections;
using Engine;

public class EnemyDummyController : MonoBehaviour {
<<<<<<< HEAD
	private Quaternion targetRotation;
	public float rotationSpeed = 450;
	public float currentwalkSpeed = 5;
	public float maxWalkSpeed = 5;
	public float runSpeed = 8;
	private CharacterController controller;
=======

	public Stats stats = new Stats();

	public GameObject HealthBar;
	public GameObject Frame;
	public float HealthBarMaxScale;
	public float HealthBarCurrentScale;

	private double _healthPointPercentage;

>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6


	// Use this for initialization
	void Start () {
		stats.CurrentHealthpoints = 20;
		stats.MaxHealthpoints = 20;
		HealthBarMaxScale = HealthBar.transform.localScale.y;
		HealthBarCurrentScale = HealthBarMaxScale;
	}
	
	// Update is called once per frame
	void Update () {

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
			}
		}
<<<<<<< HEAD
		
		Vector3 motion = input;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1) ? .7f : 1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : currentwalkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move (motion * Time.deltaTime);
=======
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
	}
}
