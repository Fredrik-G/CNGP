using UnityEngine;
using System.Collections;
using Engine;

public class CubeSpell : MonoBehaviour {

	public string Name = "";
	public double Scale { get; set; } 
	public ActiveSkill CubeActiveSkill = new ActiveSkill();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(ActiveSkill AS)
	{
		CubeActiveSkill.Name = AS.Name;
		CubeActiveSkill.Info = AS.Info;
		CubeActiveSkill.DamageHealingPower = AS.DamageHealingPower;
		CubeActiveSkill.ChiCost = AS.ChiCost;
		CubeActiveSkill.Radius = AS.Radius;
		CubeActiveSkill.SingleTarget = AS.SingleTarget;
		CubeActiveSkill.SelfTarget = AS.SelfTarget;
		CubeActiveSkill.AllyTarget = AS.AllyTarget;
		CubeActiveSkill.DoCollide = AS.DoCollide;
		CubeActiveSkill.Cooldown = AS.Cooldown;
		CubeActiveSkill.Range = AS.Range;
		CubeActiveSkill.ChannelingTime = AS.ChannelingTime;
		CubeActiveSkill.CastSpeed = AS.CastSpeed;
	}

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
