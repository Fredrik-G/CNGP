namespace Engine
{
    /// <summary>
    /// Class that contains all possible effects.
    /// </summary>
    class Effect
    {
        public bool Stun { get; set; }
        public bool Root { get; set; }
        public bool Silence { get; set; }

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

        public Effect(bool stun, bool root, bool silence, double dot, double hot, double slow)
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
