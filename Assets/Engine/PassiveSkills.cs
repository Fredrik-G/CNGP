using UnityEngine;
namespace Engine
{
    public class PassiveSkills
    {
        public string Name { get; set; }
        public double Cooldown { get; set; }

        public PassiveSkills(string name, double cooldown)
        {
            Name = name;
            Cooldown = cooldown;
        }
    }
}