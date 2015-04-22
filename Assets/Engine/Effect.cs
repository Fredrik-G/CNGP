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
        public struct Dot 
		{ 
			public double duration;
			public double dotamount;
		}

		public Dot DotStruct = new Dot();

        /// <summary>
        /// Hot heal per second.
        /// </summary>
        public struct Hot 
		{
			public double duration;
			public double hotamount;
		}

		public Hot HotStruct  = new Hot();

        /// <summary>
        /// Slow percentage.
        /// </summary>
        public struct Slow 
		{
			public double duration;
			public double slowamount;
		}

		public Slow SlowStruct = new Slow();

		public Effect(double stun, double root, double silence, double dotDuration, double dotAmount, double hotDuration, double hotAmount, double slowDuration, double slowAmount)
        {
            Stun = stun;
            Root = root;
            Silence = silence;
			DotStruct.duration = dotDuration;
			DotStruct.dotamount = dotAmount;
			HotStruct.duration = hotDuration;
			HotStruct.hotamount = hotAmount;
			SlowStruct.duration = slowDuration;
			SlowStruct.slowamount = slowAmount;


        }
    }
}
