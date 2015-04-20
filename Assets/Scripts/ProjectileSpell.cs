using UnityEngine;
using System.Collections;
using Engine;

public class ProjectileSpell : MonoBehaviour{

	public ActiveSkill ProjectileActiveSkill = new ActiveSkill();
	// Use this for initialization
	void Start (){


	}
	
	// Update is called once per framej
	void Update () {
		if (ProjectileActiveSkill.Name.CompareTo("Firestream") == 0) 
		{
			transform.localScale = Vector3.one * (float)ProjectileActiveSkill.Radius;

			ProjectileActiveSkill.Radius +=  (10 * Time.deltaTime);
		}

	}

	public void Init(ActiveSkill AS)
	{
		ProjectileActiveSkill.Name = AS.Name;
		ProjectileActiveSkill.Info = AS.Info;
		ProjectileActiveSkill.DamageHealingPower = AS.DamageHealingPower;
		ProjectileActiveSkill.ChiCost = AS.ChiCost;
		ProjectileActiveSkill.Radius = AS.Radius;
		ProjectileActiveSkill.SingleTarget = AS.SingleTarget;
		ProjectileActiveSkill.SelfTarget = AS.SelfTarget;
		ProjectileActiveSkill.AllyTarget = AS.AllyTarget;
		ProjectileActiveSkill.DoCollide = AS.DoCollide;
		ProjectileActiveSkill.Cooldown = AS.Cooldown;
		ProjectileActiveSkill.Range = AS.Range;
		ProjectileActiveSkill.ChannelingTime = AS.ChannelingTime;
		ProjectileActiveSkill.CastSpeed = AS.CastSpeed;
	}

	/// <summary>
	/// Korrigerar skillens värden enligt spelarens stats.
	/// </summary>
	/// <returns>The skill values.</returns>
	/// <param name="AS">A.</param>
	public ActiveSkill AdjustActiveSkillValues(ActiveSkill AS, PlayerStats PS)
	{
		ActiveSkill AdjustedActiveSkill = new ActiveSkill ();

		AdjustedActiveSkill.Name = AS.Name;
		AdjustedActiveSkill.Info = AS.Info;
		AdjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower * (PS.stats.Damage / 100);
		AdjustedActiveSkill.ChiCost = AS.ChiCost;
		AdjustedActiveSkill.Radius = AS.Radius * (PS.stats.Skillradius / 100);
		AdjustedActiveSkill.SingleTarget = AS.SingleTarget;
		AdjustedActiveSkill.SelfTarget = AS.SelfTarget;
		AdjustedActiveSkill.AllyTarget = AS.AllyTarget;
		AdjustedActiveSkill.DoCollide = AS.DoCollide;
		AdjustedActiveSkill.Cooldown = AS.Cooldown / (PS.stats.Cooldownduration / 100);
		AdjustedActiveSkill.Range = AS.Range * (PS.stats.Skillrange / 100);
		AdjustedActiveSkill.ChannelingTime = AS.ChannelingTime;
		AdjustedActiveSkill.CastSpeed = AS.CastSpeed;

		return AdjustedActiveSkill;
	}

}
