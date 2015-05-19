using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

public class CylinderSpell : MonoBehaviour
{
    public Vector3 OriginalScale = new Vector3(1, (float)0.3, 1);
    public string Class = "";
    public double OriginalRadius = 1;
    private double _growRate;
    public int spellTeam = 0;
    public ActiveSkill CylinderActiveSkill = new ActiveSkill();
    // Use this for initialization
    void Start()
    {
        if (CylinderActiveSkill.Name.Equals("IceFloor") || CylinderActiveSkill.Name.Equals("BlazingRing"))
        {
            transform.localScale = OriginalScale;
            _growRate = (CylinderActiveSkill.Radius - OriginalRadius) / 0.5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (CylinderActiveSkill.Name.Equals("IceFloor") || CylinderActiveSkill.Name.Equals("BlazingRing"))
        {
            transform.localScale = new Vector3((float)OriginalRadius, transform.localScale.y, (float)OriginalRadius);

            OriginalRadius += (_growRate * Time.deltaTime);
        }
        //Debug.Log ("Timeleft: " + CylinderActiveSkill.BuffEffectList[1].Effect.Timeleft);
    }

    void OnTriggerEnter(Collider other)
    {
        if (CylinderActiveSkill.Name.Equals("AirShield"))
        {
            //Bara för att jag ska kunna testa nu jämför den taggen med enemy, skall vara ens eget team egentligen
            if (other.gameObject.CompareTag("enemy"))
            {
                var enemy = other.gameObject.GetComponent<EnemyStats>();
                enemy.Heal(CylinderActiveSkill.DamageHealingPower);
            }

            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerStats>();

                var shieldOfFireBuffEffect =
                    player.EffectList.Find(x => x.Effect.Skillname == "ShieldOfFire armor increase");
                if (shieldOfFireBuffEffect != null)
                {
                    var shieldOfFireDamageBuffEffect = new BuffEffect("Increase damage by 50%",
                        new Effect("ShieldOfFire dmg increase", "Damage increase", 5, 5, 50));
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

        if (CylinderActiveSkill.Name.Equals("Bloodbending"))
        {
            if (other.gameObject.CompareTag("enemy"))
            {
                var enemy = other.gameObject.GetComponent<EnemyStats>();

                enemy.StartCoroutine(enemy.TakeDamageOverTime(CylinderActiveSkill.DamageHealingPower, 3, Class));

            }
        }
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
        CylinderActiveSkill.BuffEffectList = AS.BuffEffectList;
    }

    public ActiveSkill AdjustActiveSkillValues(ActiveSkill AS, PlayerStats PS)
    {
        var adjustedActiveSkill = new ActiveSkill();
        Class = PS.Class;
        adjustedActiveSkill.Name = AS.Name;
        adjustedActiveSkill.Info = AS.Info;
        if (AS.Name.Equals("AirShield"))
        {
            adjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower * PS.stats.HealingPowerFactor * (PS.stats.Healingpower / 100);
        }
        else
        {
            adjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower * (PS.stats.Damage / 100) * (PS.stats.DamageFactor);
        }

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
                }
            }
        }
        return adjustedActiveSkill;

    }


}