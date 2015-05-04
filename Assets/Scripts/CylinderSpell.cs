using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine;

public class CylinderSpell : MonoBehaviour {
	public Vector3 OriginalScale = new Vector3 (1, (float)0.3, 1);
	public double OriginalRadius = 1;
	private double _growRate;
	public ActiveSkill CylinderActiveSkill = new ActiveSkill();
	//private List<Collider> _colliders = new List<Collider>();
	// Use this for initialization
	void Start () {
		if (CylinderActiveSkill.Name.Equals ("Ice floor") || CylinderActiveSkill.Name.Equals ("Blazing ring")) {
			transform.localScale = OriginalScale;
			_growRate = (CylinderActiveSkill.Radius - OriginalRadius) / 0.5;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (CylinderActiveSkill.Name.Equals ("Ice floor") || CylinderActiveSkill.Name.Equals ("Blazing ring")) {
			transform.localScale = new Vector3 ((float)OriginalRadius, transform.localScale.y, (float)OriginalRadius);
			
			OriginalRadius += (_growRate * Time.deltaTime);
		}
		//Debug.Log ("Timeleft: " + CylinderActiveSkill.BuffEffectList[1].Effect.Timeleft);
	}

	/*
	void OnTriggerStay(Collider other)
	{
		if (!_colliders.Contains (other)) 
		{
			_colliders.Add(other);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (_colliders.Contains (other)) 
		{
			_colliders.Remove(other);
		}
	}

	void OnDestroy()
	{
		foreach (Collider other in _colliders) 
		{
			if(other.gameObject.CompareTag ("enemy") && CylinderActiveSkill.Name.Equals("Air vortex"))
			{
				other.gameObject.GetComponent<EnemyDummyController>().currentWalkSpeed = other.gameObject.GetComponent<EnemyDummyController>().currentWalkSpeed / (1 - (CylinderActiveSkill.BuffEffectList[0].Effect.Slow.slowamount / 100));
			}
		}
	}
*/

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
		CylinderActiveSkill.BuffEffectList = AS.BuffEffectList;
	}
	
	public ActiveSkill AdjustActiveSkillValues(ActiveSkill AS, PlayerStats PS)
	{
		var adjustedActiveSkill = new ActiveSkill ();
		
		adjustedActiveSkill.Name = AS.Name;
		adjustedActiveSkill.Info = AS.Info;
	    adjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower*(PS.stats.Damage/100);
		adjustedActiveSkill.ChiCost = AS.ChiCost;
		adjustedActiveSkill.Radius = AS.Radius * (PS.stats.Skillradius / 100);
		adjustedActiveSkill.SingleTarget = AS.SingleTarget;
		adjustedActiveSkill.SelfTarget = AS.SelfTarget;
		adjustedActiveSkill.AllyTarget = AS.AllyTarget;
		adjustedActiveSkill.DoCollide = AS.DoCollide;
		adjustedActiveSkill.Cooldown = AS.Cooldown / (PS.stats.Cooldownduration / 100);
		adjustedActiveSkill.Range = AS.Range * (PS.stats.Skillrange / 100);
		adjustedActiveSkill.ChannelingTime = AS.ChannelingTime;
		adjustedActiveSkill.CastSpeed = AS.CastSpeed;

        foreach (var item in AS.BuffEffectList)
        {
            adjustedActiveSkill.BuffEffectList.Add(
                (new BuffEffect(item.Info,
                    new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft, item.Effect.Duration,
                        item.Effect.Amount))));
        }




		if(adjustedActiveSkill.BuffEffectList.Count != 0)
		{
			for (int i = 0; i < adjustedActiveSkill.BuffEffectList.Count; i++)
			{
				if (adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Hot"))
				{
                    adjustedActiveSkill.BuffEffectList[i].Effect.Amount = adjustedActiveSkill.BuffEffectList[i].Effect.Amount * (PS.stats.Healingpower / 100);
					adjustedActiveSkill.BuffEffectList[i].Effect.Duration = adjustedActiveSkill.BuffEffectList[i].Effect.Duration * (PS.stats.Buffeffectduration / 100);
                    adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft = adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft * (PS.stats.Buffeffectduration / 100);
				}
				else
				{

					adjustedActiveSkill.BuffEffectList[i].Effect.Duration = adjustedActiveSkill.BuffEffectList[i].Effect.Duration * (PS.stats.Debuffeffectduration / 100);
                    adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft = adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft * (PS.stats.Debuffeffectduration / 100);
				}
			}
		}
		return adjustedActiveSkill;

	}
}
