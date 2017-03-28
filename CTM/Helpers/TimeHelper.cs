using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class TimeHelper
    {
        public static TimeSpan Parse(string time)
        {
            var timespan= DateTime.ParseExact(time,
                                    "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;

            return timespan;
        }

        public static int GetDuration(TimeSpan startTime,TimeSpan endTime)
        {
            var timeDifference = endTime.Subtract(startTime);

            return timeDifference.Hours * 60 + timeDifference.Minutes;

        }

        public static string FormatTime(TimeSpan timespan)
        {
            DateTime time = DateTime.Today.Add(timespan);
            string displayTime = time.ToString("hh:mm tt");

            return displayTime;
        }
    }
}
