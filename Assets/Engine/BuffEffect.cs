using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains information about one buff/effect.
    /// </summary>
	public class BuffEffect : MonoBehaviour
    {
        public int Duration { get; set; }
        public string Info { get; set; }
        public int Cooldown { get; set; }

        public Effect Effect { get; set; }

        public BuffEffect(int duration, string info, int cooldown, Effect effect)
        {
            Duration = duration;
            Info = info;
            Cooldown = cooldown;
            Effect = effect;
        }
    }
}
