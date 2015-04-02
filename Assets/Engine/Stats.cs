
namespace Engine
{

    /// <summary>
    /// Class that contains information about skill stats
    /// </summary>
    class Stats
    {

    
        public double Damage { get; set; }
        public double Chi { get; set; }

        public double Healthpoints { get; set; }
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

        public Stats(double damage, double chi, double healthpoints, double healthreg, double healingpower, double movementspeed, double armor, double physicalresistance, double magicalresistance, double buffeffectduration, double debuffeffectduration, double cooldownduration, double skillradius, double skillrange)
        {
            Damage = damage;
            Chi = chi;
            Healthpoints = healthpoints;
            Healthreg = healthreg;
            Healingpower = healingpower;
            Movementspeed = movementspeed;
            Armor = armor;
            Physicalresistance = physicalresistance;
            Magicalresistance = magicalresistance;
            Buffeffectduration = buffeffectduration;
            Buffeffectduration = debuffeffectduration;
            Cooldownduration = cooldownduration;
            Skillradius = skillradius;
            Skillrange = skillrange;
        }


 


    }
}
