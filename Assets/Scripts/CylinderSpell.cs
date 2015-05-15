using UnityEngine;
using System.Collections;
using Engine;

public class CylinderSpell : MonoBehaviour {

	public string Name = "";
    public int spellTeam = 0;
	public double Scale { get; set; } 
	public ActiveSkill CylinderActiveSkill = new ActiveSkill();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Init(ActiveSkill AS)
	{
		CylinderActiveSkill.Name = AS.Name;
		CylinderActiveSkill.Info = AS.Info;
		CylinderActiveSkill.DamageHealingPower = AS.DamageHealingPower;
		CylinderActiveSkill.ChiCost = AS.ChiCost;
		CylinderActiveSkill.Radius = AS.Radius;
		CylinderActiveSkill.SingleTarget = AS.SingleTarget;
		CylinderActiveSkill.SelfTarget = AS.SelfTarget;
		CylinderActiveSkill.AllyTarget = AS.AllyTarget;
		CylinderActiveSkill.DoCollide = AS.DoCollide;
		CylinderActiveSkill.Cooldown = AS.Cooldown;
		CylinderActiveSkill.Range = AS.Range;
		CylinderActiveSkill.ChannelingTime = AS.ChannelingTime;
		CylinderActiveSkill.CastSpeed = AS.CastSpeed;
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
