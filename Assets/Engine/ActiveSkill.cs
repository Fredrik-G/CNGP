using System;
using UnityEngine;
using System.Collections.Generic;
namespace Engine
{
    /// <summary>
    /// Class that contains information about an active skill.
    /// </summary>
    public class ActiveSkill
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public double DamageHealingPower { get; set; }
        public double ChiCost { get; set; }
        public double Radius { get; set; }
        public bool SingleTarget { get; set; }
        public bool SelfTarget { get; set; }
        public bool AllyTarget { get; set; }
        public bool DoCollide { get; set; }
        public double Cooldown { get; set; }
        public double CurrentCooldown { get; set; }
        public double Range { get; set; }
        public double ChannelingTime { get; set; }
        public double CastSpeed { get; set; }
        public int NumberOfProjectiles { get; set; }
//        public List<BuffEffect> BuffEffectList = new List<BuffEffect>();

        public ActiveSkill(string name, string info, double damageHealingPower, double chiCost, double radius, bool singleTarget, bool selfTarget, bool doCollide, bool allyTarget, double cooldown, double currentcooldown, double range, double channelingTime, double castSpeed, int numberOfProjectiles)
        {
            Name = name;
            Info = info;
            DamageHealingPower = damageHealingPower;
            ChiCost = chiCost;
            Radius = radius;
            SingleTarget = singleTarget;
            SelfTarget = selfTarget;
            AllyTarget = allyTarget;
            DoCollide = doCollide;
            CurrentCooldown = currentcooldown;
            Cooldown = cooldown;
            Range = range;
            ChannelingTime = channelingTime;
            CastSpeed = castSpeed;
            NumberOfProjectiles = numberOfProjectiles;
        }
        public ActiveSkill()
        {
            Name = "";
            Info = "";
            DamageHealingPower = 0;
            ChiCost = 0;
            Radius = 0;
            SingleTarget = false;
            SelfTarget = false;
            AllyTarget = false;
            DoCollide = false;
            CurrentCooldown = 0;
            Cooldown = 0;
            Range = 0;
            ChannelingTime = 0;
            CastSpeed = 0;
            NumberOfProjectiles = 1;
        }
    }
}