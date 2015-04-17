using System.Collections.Generic;
using System;
namespace Engine
{
    /// <summary>
    /// Class that contains information about skill stats
    /// </summary>
    public class Stats
    {  
        public double Damage { get; set; }

        public double MaxChi { get; set; }

		public double CurrentChi { get; set; }

		public double Chireg { get; set; }

		public double MaxHealthpoints { get; set; }

        public double CurrentHealthpoints { get; set; }

        public double Healthreg { get; set; }

        public double Healingpower { get; set; }

        public double Movementspeed { get; set; }

        public double Armor { get; set; }

        public double Physicalresistance { get; set; }

        public double Magicalresistance { get; set; }

        public double Buffeffectduration { get; set; }

        public double Debuffeffectduration { get; set; }

        public double Cooldownduration { get; set; }

        public double Skillradius { get; set; }

        public double Skillrange { get; set; }

		public List<ActiveSkill> SkillList = new List<ActiveSkill>();

        public Stats(double damage, double maxchi, double currentchi, double chireg, double maxhealthpoints, double currenthealthpoints, double healthreg, double healingpower, double movementspeed, double armor, double physicalresistance, double magicalresistance, double buffeffectduration, double debuffeffectduration, double cooldownduration, double skillradius, double skillrange)
        {
            Damage = damage;
            MaxChi = maxchi;
			CurrentChi = currentchi;
			Chireg = chireg;
			MaxHealthpoints = maxhealthpoints;
			CurrentHealthpoints = currenthealthpoints;
            Healthreg = healthreg;
            Healingpower = healingpower;
            Movementspeed = movementspeed;
            Armor = armor;
            Physicalresistance = physicalresistance;
            Magicalresistance = magicalresistance;
            Buffeffectduration = buffeffectduration;
            Debuffeffectduration = debuffeffectduration;
            Cooldownduration = cooldownduration;
            Skillradius = skillradius;
            Skillrange = skillrange;
        }

		public Stats()
		{
			Damage = 100;
			MaxChi = 100;
			CurrentChi = 100;
			Chireg = 100;
			MaxHealthpoints = 100;
			CurrentHealthpoints = 100;
			Healthreg = 100;
			Healingpower = 100;
			Movementspeed = 100;
			Armor = 100;
			Physicalresistance = 100;
			Magicalresistance = 100;
			Buffeffectduration = 100;
			Debuffeffectduration = 400;
			Cooldownduration = 100;
			Skillradius = 100;
			Skillrange = 100;
		}

    }
}
