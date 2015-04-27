using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysis
{
    /// <summary>
    /// Class that contains information about a user and his playtime for a certain instance.
    /// </summary>
    public class UserPlaytimeInfo
    {
        public string AccountEmail { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogOffTime { get; set; }
        public string LoginMessage { get; set; }
        public string LogOffMessage { get; set; }
        public TimeSpan PlayTime { get; set; }

        public UserPlaytimeInfo(string accountEmail)
        {
            AccountEmail = accountEmail;
            LoginMessage = "Account " + accountEmail + " logged on.";
            LogOffMessage = "Account " + accountEmail + " logged off.";
        }

        public void CalculatePlaytime()
        {
            PlayTime = LogOffTime - LoginTime;
        }
    }
}
