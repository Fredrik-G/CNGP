using System;
using System.ComponentModel;
using System.Linq;
 
namespace LogAnalysis
{
    /// <summary>
    /// Class that contains information about a user and his playtime for a certain play occasion.
    /// This class contains a list of accounts (found in logfile) and a list of an accounts play occasions.
    /// </summary>
    public class UserPlaytimeInfo
    {
        /// <summary>
        /// Class that contains information about one play occasion.
        /// </summary>
        public class PlaytimeInfo
        {
            public DateTime LoginTime { get; set; }
            public DateTime LogOffTime { get; set; }
            public TimeSpan PlayTime { get; set; }

            public void CalculatePlaytime()
            {
                PlayTime = LogOffTime - LoginTime;
            }
        }

        /// <summary>
        /// Class that contains information about an account.
        /// </summary>
        public class Account
        {
            public string AccountEmail { get; set; }
            public string LoginMessage { get; set; }
            public string LogOffMessage { get; set; }

            /// <summary>
            /// Binding list containing information about each play occasion.
            /// </summary>
            public BindingList<PlaytimeInfo> PlaytimeInfos { get; set; }

            public Account()
            {
                PlaytimeInfos = new BindingList<PlaytimeInfo>();
            }

            /// <summary>
            /// Adds a new login (play occasion).
            /// </summary>
            /// <param name="loginTime"></param>
            public void AddNewLogin(DateTime loginTime)
            {
                var playtimeInfo = new PlaytimeInfo {LoginTime = loginTime};
                PlaytimeInfos.Add(playtimeInfo);
            }

            /// <summary>
            /// Adds a log off to a certain play occasion based on given login time. 
            /// </summary>
            /// <param name="loginTime"></param>
            /// <param name="logoffTime"></param>
            public void AddNewLogoff(DateTime loginTime, DateTime logoffTime)
            {
                var playtimeInfo = PlaytimeInfos.FirstOrDefault(x => x.LoginTime == loginTime);
                playtimeInfo.LogOffTime = logoffTime;
                playtimeInfo.CalculatePlaytime();
            }

            /// <summary>
            /// Gets the login time for the latest play occasion.
            /// </summary>
            /// <returns>The login time for the latest play occasion.</returns>
            public DateTime GetLastLoginTime()
            {
                foreach (var playtimeInfo in PlaytimeInfos.Where(playtimeInfo => playtimeInfo.PlayTime.TotalMilliseconds == 0.0d))
                {
                    return playtimeInfo.LoginTime;
                }
                throw new Exception("GetLoginTime:Not found");
            }
        }

        /// <summary>
        /// Binding list containing all accounts.
        /// </summary>
        public BindingList<Account> Accounts { get; set; }

        public UserPlaytimeInfo()
        {
            Accounts = new BindingList<Account>();
        }

        /// <summary>
        /// Adds a new account with given email and login time.
        /// </summary>
        /// <param name="accountEmail">The account emil</param>
        /// <param name="loginTime">Login time for the play occasion</param>
        public void AddNewAccount(string accountEmail, DateTime loginTime)
        {
            var account = new Account
            {
                AccountEmail = accountEmail,
                LoginMessage = "Account " + accountEmail + " logged on.",
                LogOffMessage = "Account " + accountEmail + " logged off.",
            };
            account.AddNewLogin(loginTime);
            Accounts.Add(account);
        }

        /// <summary>
        /// Adds a new login (play occasion) for an existing account based on given email.
        /// </summary>
        /// <param name="accountEmail">The email to add information to</param>
        /// <param name="loginTime">The login time to add</param>
        public void AddNewLoginForExistingAccount(string accountEmail, DateTime loginTime)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountEmail == accountEmail);
            account.AddNewLogin(loginTime);
        }

        /// <summary>
        /// Adds a logoff time to an existing account based on given email.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <param name="logoffTime"></param>
        public void AddNewLogoffForExistingAccount(string accountEmail, DateTime logoffTime)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountEmail == accountEmail);
            var loginTime = account.GetLastLoginTime();
            account.AddNewLogoff(loginTime, logoffTime);
        }

        /// <summary>
        /// Checks if an account exists based on given email.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <returns></returns>
        public bool AccountExists(string accountEmail)
        {
            return Accounts.FirstOrDefault(x => x.AccountEmail == accountEmail) != null;
        }

        public void Clear()
        {
            Accounts.Clear();
        }
    }
}
