using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Class that contains information about a team.
    /// </summary>
    class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }
        private List<TurnbasedSpells> _turnbasedSpells = new List<TurnbasedSpells>(); 

        public Team(string name, int points)
        {
            Name = name;
            Points = points;
        }
    }
}
