using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains statistic information.
    /// </summary>
	public class Statistics : MonoBehaviour
    {
        public int Wins { get; set; }
        public int GamesPlayed { get; set; }
        public int Rating { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }

        public Statistics(int wins, int gamesPlayed, int rating, int kills, int deaths)
        {
            Wins = wins;
            GamesPlayed = gamesPlayed;
            Rating = rating;
            Kills = kills;
            Deaths = deaths;
        }
    }
}
