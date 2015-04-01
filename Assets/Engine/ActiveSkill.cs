namespace Engine
{
    /// <summary>
    /// Class that contains information about an active skill.
    /// </summary>
    class ActiveSkill
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public string DamageHealingPower { get; set; }
        public string ChiCost { get; set; }
        public string Radius { get; set; }
        public bool SingleTarget { get; set; }
        public bool SelfTarget { get; set; }
        public bool AllyTarget { get; set; }
        public int Cooldown { get; set; }
        public int Range { get; set; }
        public int ChannelingTime { get; set; }
        public int CastSpeed { get; set; }

        public ActiveSkill(string name, string info, string damageHealingPower, string chiCost, string radius, bool singleTarget, bool selfTarget, bool allyTarget, int cooldown, int range, int channelingTime, int castSpeed)
        {
            Name = name;
            Info = info;
            DamageHealingPower = damageHealingPower;
            ChiCost = chiCost;
            Radius = radius;
            SingleTarget = singleTarget;
            SelfTarget = selfTarget;
            AllyTarget = allyTarget;
            Cooldown = cooldown;
            Range = range;
            ChannelingTime = channelingTime;
            CastSpeed = castSpeed;
        }
    }
}
