using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    public static class DateTimeExt
    {
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

    }
}
