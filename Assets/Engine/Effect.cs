using System;
using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains all possible effects.
    /// </summary>
	public class Effect
    {
		public string Skillname;
		public string Type;
		public double Timeleft;
		public double Duration;
		public double Amount;

		public Effect(string skillname, string type, double timeleft, double duration, double amount)
        {
			Skillname = skillname;
			Type = type;
			Timeleft = timeleft;
			Duration = duration;
			Amount = amount;
        }

    }

}
