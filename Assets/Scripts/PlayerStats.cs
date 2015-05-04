using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Engine;

public class PlayerStats : MonoBehaviour {
	public Stats stats = new Stats();
    public List<BuffEffect> EffectList = new List<BuffEffect>();

    public string teamID;
	// Use this for initialization

	void Start () {
		// (Name, Info, DamageHealingPower, ChiCost, Radius, SingleTarget, SelfTarget, AllyTarget, DoCollide, Cooldown, CurrentCooldown, Range, ChannelingTime, CastSpeed, NumberOfProjectiles

        //stats.SkillList.Add(new ActiveSkill("IceRamp", "Waterbenders can manipulate ice as a means of short transportation", 0, 0.4, 0, false, false, false, false, 0.1, 0, 0, 0, 0, 0));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase movement speed by 10%", new Effect("IceRamp ms increase", "Movement speed increase", 0.1, 0.1, 200)));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase health regeneration by 100%", new Effect("IceRamp hr increase", "Health regeneration increase", 0.1, 0.1, 100)));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase healing power by 20%", new Effect("IceRamp hpow increase", "Healing power increase", 0.1, 0.1, 20)));

        //stats.SkillList.Add(new ActiveSkill("RockShoes", "Rock shoes makes the earthbender more stable and therefore stronger", 0, 25, 0, false, false, false, false, 6, 0, 0, 0, 0, 0));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Decrease movements speed by 10%", new Effect("RockShoes ms decrease", "Movement speed decrease", 6, 6, 10)));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase damage by 15%", new Effect("RockShoes dmg increase", "Damage increase", 6,6,15)));
        //stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase armor by 15%", new Effect("Rockshoes armor increase", "Armor increase", 6, 6, 15)));

        stats.SkillList.Add(new ActiveSkill("EnhancedSpeed", "When used by a skilled airbender, this technique can enable the airbender using it to travel at a speed almost too swift for the naked eye to be able to see properly. A master airbender can use this technique to briefly run across water", 0, 10, 0, false, false, false, false, 4, 0, 4.5, 0, 0, 0));
        stats.SkillList[0].BuffEffectList.Add(new BuffEffect("Increase damage by 20%", new Effect("EnhancedSeed dmg increase", "Damage increase", 2, 2, 200)));
        

		//stats.SkillList.Add (new ActiveSkill ("Earthquake", "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, false, false, true, false, 1.9, 0, 15, 0, 30, 1));
		//stats.SkillList [1].BuffEffectList.Add (new BuffEffect ("stuns the enemies", new Effect ("EarthQuake", "Stun",4,4,0)));

		//stats.SkillList.Add(new ActiveSkill ("Earthblock", "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, false, false, true, false, 2,0, 10, 0, 9, 1));

	    //stats.SkillList.Add (new ActiveSkill ("Ice floor", "a waterbender can cover a large area of the ground with ice, trapping their enemies' feet in ice", 10, 20, 7, false,false,false,false,2,0,2.5,0,0,1));
		//stats.SkillList [1].BuffEffectList.Add (new BuffEffect ("stuns the enemies", new Effect ("Ice floor", "Stun", 1, 1, 0)));

		//stats.SkillList.Add (new ActiveSkill ("Blazing ring", "Spinning kicks or sweeping arm movements create rings and arcs to slice larger, more widely spaced, or evasive targets", 16, 20, 8, false, false, false, false, 4, 0, 0, 0, 0, 0));

		//stats.SkillList.Add (new ActiveSkill ("Air vortex", "A spinning funnel of air of varying size, the air vortex can be used to trap or disorient opponents", 2, 20, 2, false, false, false, false, 5, 0, 15, 0, 0, 1));
		//stats.SkillList [1].BuffEffectList.Add (new BuffEffect ("slows the enemies", new Effect ("Air vortex","Slow",0,0,70)));

		stats.SkillList.Add(new ActiveSkill ("Airblast", "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, false, false, true, false, 1.8, 0, 5, 0, 20, 1));
		stats.SkillList.Add(new ActiveSkill ("Firestream", "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs", 0.1, 0, 0.5, false, false, false, false, 0.025, 0, 4, 0, 15,1));
		stats.SkillList.Add (new ActiveSkill ("Waterbullets", "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0, 0.3, false, false, true, false, 1.9, 0, 9, 0, 12, 5));

	}
	
	// Update is called once per frame
	void Update () {
		if (stats.CurrentChi < stats.MaxChi) {
			stats.CurrentChi += ((stats.Chireg / 100) * Time.deltaTime);
            //Debug.Log("Chi= " + stats.CurrentChi);
		}
	    if (stats.CurrentHealthpoints < stats.MaxHealthpoints)
	    {
	        stats.CurrentHealthpoints += ((stats.Healthreg*stats.HealthRegFactor*Time.deltaTime));
	    }
	}

    void FixedUpdate()
	{
		SetFactorValues ();
	}

	private void SetFactorValues()
	{
		stats.MovementSpeedFactor = 1;
	    stats.HealthRegFactor = 1;
	    stats.HealingPowerFactor = 1;
	    stats.DamageFactor = 1;
	    stats.ArmorFactor = 1;
		GetComponent<TopDownController> ().enabled = true;
		for(int i = 0; i < EffectList.Count; i++) 
		{
			if(EffectList[i].Effect.Type.Equals("Slow"))
			{
				Debug.Log("" + EffectList[i].Effect.Timeleft );
				stats.MovementSpeedFactor = stats.MovementSpeedFactor * (1 - (EffectList[i].Effect.Amount / 100));
			}

			if(EffectList[i].Effect.Type.Equals("Stun"))
			{
                GetComponent<TopDownController>().enabled = false;
				Debug.Log("" + EffectList[i].Effect.Timeleft );
			}

            if(EffectList[i].Effect.Type.Equals("Movement speed increase"))
            {
                stats.MovementSpeedFactor = stats.MovementSpeedFactor * (1 + (EffectList[i].Effect.Amount /100));
            }

            if (EffectList[i].Effect.Type.Equals("Health regeneration increase"))
            {
                stats.HealthRegFactor = stats.HealthRegFactor*(1 + (EffectList[i].Effect.Amount /100));
            }

            if (EffectList[i].Effect.Type.Equals("Healing power increase"))
            {
                stats.HealingPowerFactor = stats.HealingPowerFactor * (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Movement speed decrease"))
            {
                stats.MovementSpeedFactor = stats.MovementSpeedFactor*(1 - (EffectList[i].Effect.Amount/100));
            }

            if (EffectList[i].Effect.Type.Equals("Damage increase"))
            {
                stats.DamageFactor = stats.DamageFactor*(1 + (EffectList[i].Effect.Amount/100));
            }

            if (EffectList[i].Effect.Type.Equals("Armor increase"))
            {
                stats.ArmorFactor = stats.ArmorFactor*(1 + (EffectList[i].Effect.Amount/100));
            }


            //if för alla effekttyper..

			EffectList[i].Effect.Timeleft -= Time.deltaTime;
			

			if(EffectList[i].Effect.Timeleft <= 0)
			{
				EffectList.RemoveAt(i);
			}

		}


	}
}
