using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public static class DateConversion
    {
        public static DateTime? DayMonthStringToDate(string dateString)
        {
            var month = MonthAbreviationToNumber(dateString.Split(",")[0]);
            var day = int.Parse(dateString.Split(",")[1]);

            if (month == null) return null;

            return new DateTime(DateTime.Today.Year, month.Value, day);
        }
        public static int? MonthAbreviationToNumber(string month)
        {
            switch (month)
            {
                case "Jan": return 1;
                case "Feb": return 2;
                case "Mar": return 3;
                case "Apr": return 4;
                case "May": return 5;
                case "Jun": return 6;
                case "Jul": return 7;
                case "Aug": return 8;
                case "Sept": return 9;
                case "Oct": return 10;
                case "Nov": return 11;
                case "Dec": return 12;
            }
            return null;
        }

        internal static DateTime? DaysAgoStringToDate(string daysAgo)
        {
            var daysAgoInt = int.Parse(daysAgo.Split("d")[0]);
            DateTime.Today.AddDays(-1 * daysAgoInt);
            
        }
    }
}
