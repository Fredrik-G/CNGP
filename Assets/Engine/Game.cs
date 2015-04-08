using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that holds information about a whole game.
    /// </summary>
	public class Game : MonoBehaviour
    {
        public Map map { get; set; }
        private List<Team> _teams = new List<Team>();

        //TODO: fixa result
        public int Result { get; set; }
    }
}
