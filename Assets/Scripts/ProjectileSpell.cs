using UnityEngine;
using System.Collections;
using Engine;

public class ProjectileSpell : MonoBehaviour{
	
	public double Scale { get; set; }
    public string Class = "";
	public ActiveSkill ProjectileActiveSkill = new ActiveSkill();

    //Behövs för skillen EarthBomb
    private double _maxDamageForProjectile;

    private double _maxSlowForProjectile;

	// Use this for initialization
	void Start (){


	}
	
	// Update is called once per frame
	void Update () {
		if (ProjectileActiveSkill.Name.Equals("Firestream") || ProjectileActiveSkill.Name.Equals("StrongWind")) 
		{
			transform.localScale = Vector3.one * (float)Scale;

			Scale +=  (10 * Time.deltaTime);
		}

	    if (ProjectileActiveSkill.Name.Equals("EarthBomb"))
	    {
	        var timeInAir = ProjectileActiveSkill.Range/ProjectileActiveSkill.CastSpeed;
	        var minDamage = _maxDamageForProjectile/3;
            ProjectileActiveSkill.DamageHealingPower -= ((_maxDamageForProjectile - minDamage) / timeInAir) * Time.deltaTime;
	    }

	    if (ProjectileActiveSkill.Name.Equals("StrongWind"))
	    {
	        //ProjectileActiveSkill.BuffEffectList[0].Effect.Amount 
	    }
	}

	public void Init(ActiveSkill AS)
	{
		ProjectileActiveSkill.Name = AS.Name;
		ProjectileActiveSkill.Info = AS.Info;
		ProjectileActiveSkill.DamageHealingPower = AS.DamageHealingPower;
	    _maxDamageForProjectile = AS.DamageHealingPower;
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
	    ProjectileActiveSkill.BuffEffectList = AS.BuffEffectList;
	}


    /// <summary>
    /// Korrigerar skillens värden enligt spelarens stats.
    /// </summary>
    /// <param name="AS"></param>
    /// <param name="PS"></param>
    /// <returns></returns>
	public ActiveSkill AdjustActiveSkillValues(ActiveSkill AS, PlayerStats PS)
	{
		var adjustedActiveSkill = new ActiveSkill ();
        Class = PS.Class;
		adjustedActiveSkill.Name = AS.Name;
		adjustedActiveSkill.Info = AS.Info;
	    adjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower*(PS.stats.Damage/100)*(PS.stats.DamageFactor);
		adjustedActiveSkill.ChiCost = AS.ChiCost;
		adjustedActiveSkill.Radius = AS.Radius * (PS.stats.Skillradius / 100);
		adjustedActiveSkill.SingleTarget = AS.SingleTarget;
		adjustedActiveSkill.SelfTarget = AS.SelfTarget;
		adjustedActiveSkill.AllyTarget = AS.AllyTarget;
		adjustedActiveSkill.DoCollide = AS.DoCollide;
		adjustedActiveSkill.Cooldown = AS.Cooldown / (PS.stats.Cooldownduration / 100);
		adjustedActiveSkill.Range = AS.Range * (PS.stats.Skillrange / 100) * PS.stats.SkillrangeFactor;
		adjustedActiveSkill.ChannelingTime = AS.ChannelingTime;
		adjustedActiveSkill.CastSpeed = AS.CastSpeed;

        foreach (var item in AS.BuffEffectList)
        {
            adjustedActiveSkill.BuffEffectList.Add(
                (new BuffEffect(item.Info,
                    new Effect(item.Effect.Skillname, item.Effect.Type, item.Effect.Timeleft, item.Effect.Duration,
                        item.Effect.Amount))));
        }

        if (adjustedActiveSkill.BuffEffectList.Count != 0)
        {
            for (int i = 0; i < adjustedActiveSkill.BuffEffectList.Count; i++)
            {
                if (adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Hot"))
                {
                    adjustedActiveSkill.BuffEffectList[i].Effect.Amount = adjustedActiveSkill.BuffEffectList[i].Effect.Amount * (PS.stats.Healingpower / 100);
                    adjustedActiveSkill.BuffEffectList[i].Effect.Duration = adjustedActiveSkill.BuffEffectList[i].Effect.Duration * (PS.stats.Buffeffectduration / 100);
                    adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft = adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft * (PS.stats.Buffeffectduration / 100);
                }
                else if (adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Stun") || adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Slow") || adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Dot") || adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Root"))
                {
                    adjustedActiveSkill.BuffEffectList[i].Effect.Duration = adjustedActiveSkill.BuffEffectList[i].Effect.Duration * (PS.stats.Debuffeffectduration / 100);
                    adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft = adjustedActiveSkill.BuffEffectList[i].Effect.Timeleft * (PS.stats.Debuffeffectduration / 100);

                    if (adjustedActiveSkill.BuffEffectList[i].Effect.Type.Equals("Slow"))
                    {
                        adjustedActiveSkill.BuffEffectList[i].Effect.Amount = _maxSlowForProjectile;
                    }
                }
            }
        }

		return adjustedActiveSkill;
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerStats>();

            var shieldOfFireBuffEffect = player.EffectList.Find(x => x.Effect.Skillname == "ShieldOfFire armor increase");
            if (shieldOfFireBuffEffect != null)
            {
                var shieldOfFireDamageBuffEffect = new BuffEffect("Increase damage by 50%", new Effect("ShieldOfFire dmg increase", "Damage increase", 5, 5, 50));
                var foundEffect =
                    player.EffectList.Find(x => x.Effect.Skillname == shieldOfFireDamageBuffEffect.Effect.Skillname);

                if (foundEffect == null)
                {
                    player.EffectList.Add(shieldOfFireDamageBuffEffect);
                }
                else
                {
                    if (foundEffect.Effect.Timeleft < shieldOfFireDamageBuffEffect.Effect.Timeleft)
                    {
                        player.EffectList.Remove(foundEffect);
                        player.EffectList.Add(shieldOfFireDamageBuffEffect);
                    }
                }
            }
        }
    }

}
