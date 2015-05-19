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

        public double DamageFactor { get; set; }

        public double MaxChi { get; set; }

		public double CurrentChi { get; set; }

		public double Chireg { get; set; }

		public double MaxHealthpoints { get; set; }

        public double CurrentHealthpoints { get; set; }

        public double Healthreg { get; set; }

        public double HealthRegFactor { get; set; }

        public double Healingpower { get; set; }

        public double HealingPowerFactor { get; set; }

        public double Movementspeed { get; set; }

		public double MovementSpeedFactor { get; set; }

        public double Armor { get; set; }

        public double ArmorFactor { get; set; }

        public double Physicalresistance { get; set; }

        public double PhysicalresistanceFactor { get; set; }

        public double Magicalresistance { get; set; }

        public double MagicalresistanceFactor { get; set; }

        public double Buffeffectduration { get; set; }

        public double Debuffeffectduration { get; set; }

        public double Cooldownduration { get; set; }

        public double Skillradius { get; set; }

        public double Skillrange { get; set; }

        public double SkillrangeFactor { get; set; }

        public int SpendablePoints { get; set; }

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
			SpendablePoints = 100;
			MovementSpeedFactor = 1;
            HealthRegFactor = 1;
            HealingPowerFactor = 1;
            DamageFactor = 1;
            ArmorFactor = 1;
            SkillrangeFactor = 1;
            MagicalresistanceFactor = 1;
            PhysicalresistanceFactor = 1;
        }

		public Stats()
		{
			SpendablePoints = 100;
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
			Debuffeffectduration = 100;
			Cooldownduration = 100;
			Skillradius = 100;
			Skillrange = 100;

			MovementSpeedFactor = 1;
            HealthRegFactor = 1;
            HealingPowerFactor = 1;
		    DamageFactor = 1;
		    ArmorFactor = 1;
		    SkillrangeFactor = 1;
            MagicalresistanceFactor = 1;
            PhysicalresistanceFactor = 1;
		}

        public void IncreasePower()
        {
            if (SpendablePoints > 0)
            {

                Damage = Damage + 3;
                Physicalresistance = Physicalresistance + 1;
                Skillradius = Skillradius + 2;
                MaxChi = MaxChi + 1;

                SpendablePoints--;
            }

        }

        public void IncreaseAgility()
        {
            if (SpendablePoints > 0)
            {
                Movementspeed = Movementspeed + 1.5;
                Cooldownduration = Cooldownduration + 0.5;
                Skillrange = Skillrange + 2;
                SpendablePoints--;
            }
        }
        public void IncreaseHarmony()
        {
            if (SpendablePoints > 0)
            {
                Damage = Damage + 1;
                Healingpower = Healingpower + 3;
                Buffeffectduration = Buffeffectduration + 1;
                Magicalresistance = Magicalresistance + 2;
                Chireg = Chireg + 3;
                SpendablePoints--;
            }
        }
        public void IncreaseToughness()
        {
            if (SpendablePoints > 0)
            {
                Armor = Armor + 2;
                Physicalresistance = Physicalresistance + 2;
                Healthreg = Healthreg + 4;
                SpendablePoints--;
            }
        }
        public void IncreaseEndurance()
        {
            if (SpendablePoints > 0)
            {
                MaxHealthpoints = MaxHealthpoints + 2;
                Healthreg = Healthreg + 4;
                Magicalresistance = Magicalresistance + 1;
                SpendablePoints--;
            }
        }
        public void IncreaseWisdom()
        {
            if (SpendablePoints > 0)
            {
                MaxChi = MaxChi + 4;
                Chireg = Chireg + 4;
                Debuffeffectduration = Debuffeffectduration + 0.5;
                Cooldownduration = Cooldownduration + 1;
                SpendablePoints--;
            }
        }
        public void IncreaseVersatility()
        {
            if (SpendablePoints > 0)
            {
                Buffeffectduration = Buffeffectduration + 2;
                Debuffeffectduration = Debuffeffectduration + 2;
                Damage = Damage + 1;
                Healingpower = Healingpower + 1;
                SpendablePoints--;
            }
        }

    }
}
