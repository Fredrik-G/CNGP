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
        public bool DoCollide { get; set; }
        public double Cooldown { get; set; }
        public double CurrentCooldown { get; set; }
        public double Range { get; set; }
        public double ChannelingTime { get; set; }
        public double CastSpeed { get; set; }
        public int NumberOfProjectiles { get; set; }

        public ActiveSkill(string name, string info, double damageHealingPower, double chiCost, double radius, bool doCollide, double cooldown, double currentcooldown, double range, double channelingTime, double castSpeed, int numberOfProjectiles)
        {
            Name = name;
            Info = info;
            DamageHealingPower = damageHealingPower;
            ChiCost = chiCost;
            Radius = radius;
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
