using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsLibrary
{
    public static class DateConversion
    {
        private static readonly int moreThan30Days = 31;
        public static DateTime? MonthDayStringToDate(string? dateString)
        {
            if (dateString == null) return null;
            try
            {
                var month = MonthAbreviationToNumber(dateString.Split(",")[0]);
                var day = int.Parse(dateString.Split(",")[1]);

                if (month == null) return null;

                return new DateTime(DateTime.Today.Year, month.Value, day);
            }
            catch { return null; }
        }
        internal static int? MonthAbreviationToNumber(string month)
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

        public static DateTime? TimeAgoStringToDate(string? daysAgo)
        {
            if (daysAgo == null) return null;
            try
            {
                if (daysAgo.Contains("+") || daysAgo.Contains("month")) return DateTime.Today.AddDays(-1 * moreThan30Days);
                if (daysAgo.Contains("h")) return DateTime.Today;

                var daysAgoInt = int.Parse(daysAgo.Split("d")[0]);
                return DateTime.Today.AddDays(-1 * daysAgoInt);
            }
            catch { return null; }
        }

        public static string DateTimeToTimeAgo(DateTime? date)
        {
            if (date == null) return "";

            var dayDifference = (DateTime.Now - date.Value).TotalDays;

            if (dayDifference >= 31)
                return "+30d ago";
            if (dayDifference >= 1 && dayDifference < 31)
                return $"{dayDifference}d ago";
            if (dayDifference < 1)
                return $"{(int)( dayDifference * 24)}h ago";
            return "";
        }
    }
}
