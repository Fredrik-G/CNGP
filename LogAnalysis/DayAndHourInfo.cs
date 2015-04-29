using System;
using System.ComponentModel;

namespace LogAnalysis
{
    /// <summary>
    /// Class containing lists of day and hour information.
    /// </summary>
    public class DayAndHourInfo
    {
        #region Day and Hour Classes

        /// <summary>
        /// Class for information about a day.
        /// </summary>
        public class DayInfo
        {
            public DayOfWeek DayOfWeek { get; set; }
            public int Count { get; set; }

            public DayInfo(DayOfWeek dayOfWeek, int count)
            {
                DayOfWeek = dayOfWeek;
                Count = count;
            }
        }

        /// <summary>
        /// Class for information about a hour.
        /// </summary>
        public class HourInfo
        {
            public int Hour { get; set; }
            public int Count { get; set; }

            public HourInfo(int hour, int count)
            {
                Hour = hour;
                Count = count;
            }
        }

        #endregion

        #region Data

        private readonly BindingList<DayInfo> _dayInfos = new BindingList<DayInfo>();
        private readonly BindingList<HourInfo> _hourInfos = new BindingList<HourInfo>();

        #endregion

        public void AddDay(DayOfWeek dayOfWeek, int count)
        {
            _dayInfos.Add(new DayInfo(dayOfWeek, count));
        }
        public void AddHour(int hour, int count)
        {
            _hourInfos.Add(new HourInfo(hour, count));
        }

        public BindingList<DayInfo> GetDayInfos()
        {
            return _dayInfos;
        }
        public BindingList<HourInfo> GetHourInfos()
        {
            return _hourInfos;
        }

        /// <summary>
        /// Removes all elements from _dayInfos & _hourInfos.
        /// </summary>
        public void Clear()
        {
            _dayInfos.Clear();
            _hourInfos.Clear();
        }
    }
}
