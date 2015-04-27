using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace LogAnalysis
{
    public class LogAnalysis
    {
        #region Data

        private readonly List<LogRowInfo> _gameplayLogRowInfos = new List<LogRowInfo>();
        private readonly List<LogRowInfo> _generalLogRowInfos = new List<LogRowInfo>();
        private readonly DayAndHourInfo _dayAndHourInfo = new DayAndHourInfo();

        #endregion

        /// <summary>
        /// Groups and sorts the gameplay log list based on most frequent days/hours.
        /// </summary>
        public void SortMostFrequentDayAndHour()
        {
            var days = new DayOfWeek[_gameplayLogRowInfos.Count];
            var hours = new int[_gameplayLogRowInfos.Count];

            for (var i = 0; i < _gameplayLogRowInfos.Count; i++)
            {
                days[i] = _gameplayLogRowInfos[i].DateTime.DayOfWeek;
                hours[i] = _gameplayLogRowInfos[i].DateTime.Hour;
            }

            //Groups and sorts the day of the week 
            var sortedDays = (from day in days
                group day by day into groupedDays
                orderby groupedDays.Count() descending
                select new { DayOfWeek = groupedDays.Key, Count = groupedDays.Count() });

            //Groups and sorts the hour of the day 
            var sortedHours = (from hour in hours
                               group hour by hour into groupedHours
                               orderby groupedHours.Count() descending
                               select new { Hour = groupedHours.Key, Count = groupedHours.Count() });
                       
            //Goes through every day in sortedDays to add them to the bindinglist.
            foreach (var day in sortedDays)
            {
                _dayAndHourInfo.AddDay(day.DayOfWeek, day.Count);
            }
            foreach (var hour in sortedHours)
            {
                _dayAndHourInfo.AddHour(hour.Hour, hour.Count);
            }   
        }

        /// <summary>
        /// Reads a log file and saves each row in a list.
        /// </summary>
        /// <param name="fileLocation">The log file location.</param>
        /// <param name="isGameplayLog">Tells the function that this log file is a gameplay log file.</param>
        public void ReadLogFile(string fileLocation, bool isGameplayLog)
        {
            //const string fileLocation = AppDomain.CurrentDomain.BaseDirectory + "/logs/";
            //const string fileName = "logfile.log";
            //const string fileLocation = "M:/Desktop/år2/SystemProgramvaruutveckling/UDPLog/UDPLog/UDPLog/bin/Debug/logs/";         
            const Int32 bufferSize = 128;

            using (var fileStream = File.OpenRead(fileLocation + " "))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
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

        /// <summary>
        /// Calculates every users play time.
        /// </summary>
        /// <returns></returns>
        public BindingList<UserPlaytimeInfo> CalculateUserPlayTime()
        {
            var debugLevelName = log4net.Core.Level.Debug + " ";
            var userInfoRows = _gameplayLogRowInfos.Where(row => String.Equals(debugLevelName, row.Level)).ToList();
            //same as foreach(var row in _gameplayLogRowInfos){ if(String.Equals(debugLevelName, row.Level)) userInfoRows.Add(row);}

            var userPlaytimeInfos = new BindingList<UserPlaytimeInfo>();

            foreach (var userInfoRow in userInfoRows)
            {
                if (userInfoRow.IsLoginMessage())
                {
                    var userPlaytimeInfo = new UserPlaytimeInfo(userInfoRow.GetEmailFromMessage())
                    {
                        LoginTime = userInfoRow.DateTime
                    };
                    userPlaytimeInfos.Add(userPlaytimeInfo);
                }
                if (userInfoRow.IsLogoffMessage())
                {
                    var user = userPlaytimeInfos.FirstOrDefault(x => x.AccountEmail == userInfoRow.GetEmailFromMessage());
                    user.LogOffTime = userInfoRow.DateTime;
                    user.CalculatePlaytime();
                }
            }

            return userPlaytimeInfos;
        }

        /// <summary>
        /// Gets the BindingList containing day information
        /// </summary>
        /// <returns>The BindingList containg day information</returns>
        public BindingList<DayAndHourInfo.DayInfo> GetDaysInfo()
        {
            return _dayAndHourInfo.GetDayInfos();
        }
        /// <summary>
        /// Gets the BindingList containing hour information
        /// </summary>
        /// <returns>The BindingList containg hour information</returns>
        public BindingList<DayAndHourInfo.HourInfo> GetHoursInfos()
        {
            return _dayAndHourInfo.GetHourInfos();
        }
    }
}