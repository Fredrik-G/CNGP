using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains information about one buff/effect.
    /// </summary>
	public class BuffEffect
    {
        public string Info { get; set; }
        public Effect Effect { get; set; }

        public BuffEffect(string info, Effect effect)
        {
            Info = info;
            Effect = effect;
        }
    }
}
