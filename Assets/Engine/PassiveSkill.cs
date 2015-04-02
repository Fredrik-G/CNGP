
namespace Engine
{
    class PassiveSkill
    {
        public string Name { get; set; }
        public double Cooldown { get; set; }


        public PassiveSkill(string name, double cooldown)
        {
            Name = name;
            Cooldown = cooldown;
        }
    }
}
