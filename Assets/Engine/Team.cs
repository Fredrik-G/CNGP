using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains information about a team.
    /// </summary>
	public class Team : MonoBehaviour
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
