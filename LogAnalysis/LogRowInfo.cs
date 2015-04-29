using System;

namespace LogAnalysis
{
    /// <summary>
    /// Class containing information about one row from a log file.
    /// </summary>
    public class LogRowInfo
    {
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Formats a given line and saves relevant values.
        /// </summary>
        /// <param name="line"></param>
        public void FormatLine(string line)
        {
            var splitLine = line.Split(';');
            var date = splitLine[0].Split(' ')[0].Split('-');        
            var time = splitLine[0].Split(' ')[1].Split(':');

            Thread = splitLine[1];
            Level = splitLine[2];
            Logger = splitLine[3];
            Message = splitLine[5];

            var year = Convert.ToInt16(date[0]);
            var month = Convert.ToInt16(date[1]);
            var day = Convert.ToInt16(date[2]);

            var hour = Convert.ToInt16(time[0]);
            var minutes = Convert.ToInt16(time[1]);
            var seconds = Convert.ToInt16(time[2].Split(',')[0]);
            var milliseconds = Convert.ToInt16(time[2].Split(',')[1]);
            DateTime = new DateTime(year, month, day, hour, minutes, seconds, milliseconds);
        }

        public bool IsLoginMessage()
        {
            return Message.EndsWith("on.");
        }
        public bool IsLogoffMessage()
        {
            return Message.EndsWith("off.");
        }

        public string GetEmailFromMessage()
        {
            var splittedMessage = Message.Split(' ');
            return String.Equals(splittedMessage[0], "Account") ? splittedMessage[1] : String.Empty;
        }
    }
}
