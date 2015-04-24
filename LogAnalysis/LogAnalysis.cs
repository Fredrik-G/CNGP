using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogAnalysis
{
    public class LogAnalysis
    {
        private readonly List<LogRowInfo> _rowInfos = new List<LogRowInfo>();


        public void CalculateMostFrequentDay()
        {
            var days = new DayOfWeek[_rowInfos.Count];

            for (var i = 0; i < _rowInfos.Count; i++)
            {
                days[i] = _rowInfos[i].DateTime.DayOfWeek;
            }

            //Groups and sorts the day of the week 
            var sortedDays = (from day in days
                group day by day
                into groupedDays
                orderby groupedDays.Count() descending
                select new {DayOfWeek = groupedDays.Key, Count = groupedDays.Count()});

        }

        public void ReadLogFile()
        {
            // var fileLocation = AppDomain.CurrentDomain.BaseDirectory + "/logs/";
            const string fileLocation = "M:/Desktop/år2/SystemProgramvaruutveckling/UDPLog/UDPLog/UDPLog/bin/Debug/logs/";
            const string fileName = "logfile.log";
            const Int32 bufferSize = 128;

            using (var fileStream = File.OpenRead(fileLocation + fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var rowInfo = new LogRowInfo();
                    rowInfo.FormatLine(line);
                    _rowInfos.Add(rowInfo);
                }
            }
        }
    }
}