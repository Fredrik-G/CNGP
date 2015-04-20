using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains all possible effects.
    /// </summary>
	public class Effect 
    {
		/// <summary>
		/// Stunduration.
		/// </summary>
		/// <value>The stun.</value>
        public double Stun { get; set; }

		/// <summary>
		/// Rootduration.
		/// </summary>
		/// <value>The root.</value>
        public double Root { get; set; }

		/// <summary>
		/// Silenceduration.
		/// </summary>
		/// <value>The silence.</value>
        public double Silence { get; set; }

        /// <summary>
        /// Dot damage per second.
        /// </summary>
        public double Dot { get; set; }

        /// <summary>
        /// Hot heal per second.
        /// </summary>
        public double Hot { get; set; }

        /// <summary>
        /// Slow percentage.
        /// </summary>
        public double Slow { get; set; }

		public Effect(double stun, double root, double silence, double dot, double hot, double slow)
        {
            Stun = stun;
            Root = root;
            Silence = silence;
            Dot = dot;
            Hot = hot;
            Slow = slow;
        }
    }
}
