using UnityEngine;
using System.Collections;
using Engine;

public class CylinderSpell : MonoBehaviour {

<<<<<<< HEAD
	public Vector3 OriginalScale = new Vector3 (1, (float)0.3, 1);
	public double OriginalRadius = 1;
	private double GrowRate;
	public ActiveSkill CylinderActiveSkill = new ActiveSkill();
	// Use this for initialization
	void Start () {
		if (CylinderActiveSkill.Name.Equals ("Ice floor") || CylinderActiveSkill.Name.Equals ("Blazing ring"))
		{
			transform.localScale = OriginalScale;
			GrowRate = (CylinderActiveSkill.Radius - OriginalRadius) / 0.5;
		}

=======
	public string Name = "";
	public double Scale { get; set; } 
	public ActiveSkill CylinderActiveSkill = new ActiveSkill();
	// Use this for initialization
	void Start () {
		
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if (CylinderActiveSkill.Name.Equals("Ice floor") || CylinderActiveSkill.Name.Equals("Blazing ring")) 
		{
			transform.localScale = new Vector3((float)OriginalRadius, transform.localScale.y, (float)OriginalRadius);
			
			OriginalRadius +=  (GrowRate * Time.deltaTime);
		}
=======
		
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
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
<<<<<<< HEAD
		AdjustedActiveSkill.BuffEffectList = AS.BuffEffectList;

		if(AdjustedActiveSkill.BuffEffectList.Count != 0)
		{
			for(int i = 0; i < AdjustedActiveSkill.BuffEffectList.Count; i++)
			{
				if(AdjustedActiveSkill.BuffEffectList[i].Effect.Stun > 0)
				{
					AdjustedActiveSkill.BuffEffectList[i].Effect.Stun = AdjustedActiveSkill.BuffEffectList[i].Effect.Stun * (PS.stats.Debuffeffectduration / 100);
				}

				if(AdjustedActiveSkill.BuffEffectList[i].Effect.Root > 0)
				{
					AdjustedActiveSkill.BuffEffectList[i].Effect.Root = AdjustedActiveSkill.BuffEffectList[i].Effect.Root * (PS.stats.Debuffeffectduration / 100);
				}

				if(AdjustedActiveSkill.BuffEffectList[i].Effect.Silence > 0)
				{
					AdjustedActiveSkill.BuffEffectList[i].Effect.Silence = AdjustedActiveSkill.BuffEffectList[i].Effect.Silence * (PS.stats.Debuffeffectduration / 100);
				}

				if(AdjustedActiveSkill.BuffEffectList[i].Effect.DotStruct.dotamount > 0)
				{
					//FIXA
				}

				if(AdjustedActiveSkill.BuffEffectList[i].Effect.HotStruct.hotamount > 0)
				{
					//FIXA
				}

				if(AdjustedActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount > 0)
				{
					AdjustedActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount = (1 - (( 100 - AdjustedActiveSkill.BuffEffectList[i].Effect.SlowStruct.slowamount) / PS.stats.Debuffeffectduration)) * 100;
				}
			}
		}
=======
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
		
		return AdjustedActiveSkill;
	}
}
