using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains all possible effects.
    /// </summary>
	public class Effect : MonoBehaviour
    {
        public bool Stun { get; set; }
        public bool Root { get; set; }
        public bool Silence { get; set; }

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

<<<<<<< HEAD
		public Effect(double stun, double root, double silence, double dotDuration, double dotAmount, double hotDuration, double hotAmount, double slowDuration, double slowAmount)
=======
        public Effect(bool stun, bool root, bool silence, double dot, double hot, double slow)
>>>>>>> 2b43b92b4df6773d8f06fb2a36599a1f6ccdedb6
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
