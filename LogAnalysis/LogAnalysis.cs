using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using log4net.Core;

namespace LogAnalysis
{
    public class LogAnalysis
    {
        #region Data

        private readonly List<LogRowInfo> _gameplayLogRowInfos = new List<LogRowInfo>();
        private readonly List<LogRowInfo> _generalLogRowInfos = new List<LogRowInfo>();
        private readonly DayAndHourInfo _dayAndHourInfo = new DayAndHourInfo();
        private readonly UserPlaytimeInfo _userPlaytimeInfo = new UserPlaytimeInfo();
        private List<IGrouping<double, UserPlaytimeInfo.PlaytimeInfo>> _playOccasions = new List<IGrouping<double, UserPlaytimeInfo.PlaytimeInfo>>();

        #endregion

        #region Log Analysis Methods

        /// <summary>
        /// Groups and sorts the gameplay log list based on most frequent days/hours.
        /// </summary>
        public void SortMostFrequentDayAndHour()
        {
            _dayAndHourInfo.Clear();

            //Groups day of the week
            var groupedDays = _gameplayLogRowInfos.GroupBy(x => x.DateTime.DayOfWeek);
            foreach (var day in groupedDays)
            {
                _dayAndHourInfo.AddDay(day.Key, day.Count());
            }

            //Groups hour of day
            var gruopedHours = _gameplayLogRowInfos.GroupBy(x => x.DateTime.Hour);
            foreach (var hour in gruopedHours)
            {
                _dayAndHourInfo.AddHour(hour.Key, hour.Count());
            }
        }

        /// <summary>
        /// Calculates every users play time.
        /// </summary>
        /// <returns></returns>
        public void CalculateUserPlayTime()
        {
            _userPlaytimeInfo.Clear();

            var debugLevelName = Level.Debug + " ";
            var userInfoRows = _gameplayLogRowInfos.Where(row => String.Equals(debugLevelName, row.Level)).ToList();
            //same as foreach(var row in _gameplayLogRowInfos){ if(String.Equals(debugLevelName, row.Level)) userInfoRows.Add(row);}

            foreach (var userInfoRow in userInfoRows)
            {
                if (userInfoRow.IsLoginMessage())
                {
                    var accountEmail = userInfoRow.GetEmailFromMessage();
                    if (!_userPlaytimeInfo.AccountExists(accountEmail))
                    {
                        _userPlaytimeInfo.AddNewAccount(accountEmail, userInfoRow.DateTime);
                    }
                    else
                    {
                        _userPlaytimeInfo.AddNewLoginForExistingAccount(accountEmail, userInfoRow.DateTime);
                    }
                }
                if (userInfoRow.IsLogoffMessage())
                {
                    var accountEmail = userInfoRow.GetEmailFromMessage();
                    if (_userPlaytimeInfo.AccountExists(accountEmail))
                    {
                        _userPlaytimeInfo.AddNewLogoffForExistingAccount(accountEmail, userInfoRow.DateTime);
                    }
                }
            }
        }

        /// <summary>
        /// Groups every users play occasions
        /// and sort it based on the count.
        /// Count = number of times that a certain play time (for example 2h) is found.
        /// Sort = Descending, highest count to lowest.
        /// </summary>
        /// <param name="numberOfPlayTimesToList">How many play times to add to the list. Adds every element if this variable is set to 0. Default value = 0</param>
        public void SortUsersPlayTime(int numberOfPlayTimesToList = 0)
        {
            //Groups all play occasions found for every account.
            if (numberOfPlayTimesToList == 0)
            {
                _playOccasions = _userPlaytimeInfo.Accounts
                    .SelectMany(account => account.PlaytimeInfos)
                    .ToList()
                    .GroupBy(x => x.PlayTime)
                    .OrderByDescending(x => x.Count())
                    .ToList();
            }
            else
            {
                _playOccasions = _userPlaytimeInfo.Accounts
                    .SelectMany(account => account.PlaytimeInfos)
                    .ToList()
                    .GroupBy(x => x.PlayTime)
                    .OrderByDescending(x => x.Count())
                    .Take(numberOfPlayTimesToList)
                    .ToList();
            }
        }

        #endregion

        #region Getters

        /// <summary>
        /// Gets the BindingList containing day information.
        /// </summary>
        /// <returns>The BindingList containg day information</returns>
        public BindingList<DayAndHourInfo.DayInfo> GetDaysInfo()
        {
            return _dayAndHourInfo.GetDayInfos();
        }

        /// <summary>
        /// Gets the BindingList containing hour information.
        /// </summary>
        /// <returns>The BindingList containg hour information</returns>
        public BindingList<DayAndHourInfo.HourInfo> GetHoursInfos()
        {
            return _dayAndHourInfo.GetHourInfos();
        }

        /// <summary>
        /// Gets the BindingList containing user play time information.
        /// </summary>
        /// <returns>The BindingList containing user play time information</returns>
        public BindingList<UserPlaytimeInfo.Account> GetUserPlaytime()
        {
            return _userPlaytimeInfo.Accounts;
        }

        /// <summary>
        /// Gets the BindingList containing a given user's play time information.
        /// </summary>
        /// <param name="accountEmail">The user account email to get information for.</param>
        /// <returns>The BindingList containing user play time information</returns>
        public BindingList<UserPlaytimeInfo.PlaytimeInfo> GetPlaytimeForUser(string accountEmail)
        {
            var account = _userPlaytimeInfo.Accounts.FirstOrDefault(x => x.AccountEmail == accountEmail);
            return account.PlaytimeInfos;
        }

        /// <summary>
        /// Gets the list containing users play time information.
        /// </summary>
        /// <returns>The list containing users play time information.</returns>
        public List<IGrouping<double, UserPlaytimeInfo.PlaytimeInfo>> GetUsersPlayOccasions()
        {
            return _playOccasions;
        }

        #endregion

        /// <summary>
        /// Reads a log file and saves each row in a list.
        /// </summary>
        /// <param name="fileLocation">The log file location.</param>
        /// <param name="isGameplayLog">Tells the function that this log file is a gameplay log file.</param>
        public void ReadLogFile(string fileLocation, bool isGameplayLog)
        {
            //Clear necessary lists.
            _playOccasions.Clear();
            if (isGameplayLog)
            {
                _gameplayLogRowInfos.Clear();
            }
            else
            {
                _generalLogRowInfos.Clear();
            }

            //const string fileLocation = AppDomain.CurrentDomain.BaseDirectory + "/logs/";    
            const Int32 bufferSize = 128;

            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(fileLocation);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
                {
                    fileStream = null;
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var rowInfo = new LogRowInfo();
                        rowInfo.FormatLine(line);

                        if (isGameplayLog)
                        {
                            _gameplayLogRowInfos.Add(rowInfo);
                        }
                        else
                        {
                            _generalLogRowInfos.Add(rowInfo);
                        }
                    }
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
        }
    }
}