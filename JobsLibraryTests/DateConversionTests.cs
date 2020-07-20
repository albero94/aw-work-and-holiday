using System;
using Xunit;
using JobsLibrary;

namespace JobsLibraryTests
{
    public class DateConversionTests
    {
        #region DateTimeToTime
        [Theory]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(1)]
        public void DateTimeToTime_1to30Days_ReturnsDaysAgo(int dayDifference)
        {
            var candidate = DateTime.Today.AddDays(-dayDifference);
            var result = DateConversion.DateTimeToTimeAgo(candidate);
            Assert.Equal($"{dayDifference}d ago", result);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(8)]
        [InlineData(23)]
        public void DateTimeToTime_1to23Hours_ReturnsHoursAgo(int hourDifference)
        {
            var candidate = DateTime.Today.AddHours(-hourDifference);
            var result = DateConversion.DateTimeToTimeAgo(candidate);
            Assert.Equal($"{hourDifference}h ago", result);
        }

        [Theory]
        [InlineData(31)]
        [InlineData(60)]
        public void DateTimeToTime_MoreThan30Days_ReturnsPlus30DaysAgo(int dayDifference)
        {
            var candidate = DateTime.Today.AddDays(-dayDifference);
            var result = DateConversion.DateTimeToTimeAgo(candidate);
            Assert.Equal($"+30d ago", result);
        }
        #endregion

        #region MonthDayStringToDate
        [Theory]
        [InlineData("May, 17")]
        [InlineData("Sept, 25")]
        [InlineData("Aug, 31")]
        public void MonthDayStringToDate_DayAndMonth_ReturnsDate(string candidate)
        {
            var result = DateConversion.MonthDayStringToDate(candidate);
            Assert.IsType<DateTime>(result);
        }

        [Theory]
        [InlineData("May")]
        [InlineData("September, 25")]
        [InlineData("2020 Aug, 31")]
        public void MonthDayStringToDate_NotDayAndMonth_ReturnsNull(string candidate)
        {
            var result = DateConversion.MonthDayStringToDate(candidate);
            Assert.Null(result);
        }

        [Fact]
        public void MonthDayStringToDate_May17_Date()
        {
            var stringDate = "May, 17";
            var result = DateConversion.MonthDayStringToDate(stringDate);

            Assert.Equal(new DateTime(DateTime.Now.Year, 05, 17), result);
        }

        [Fact]
        public void MonthDayStringToDate_Sept30_Date()
        {
            var stringDate = "Sept, 30";
            var result = DateConversion.MonthDayStringToDate(stringDate);

            Assert.Equal(new DateTime(DateTime.Now.Year, 09, 30), result);
        }

        [Fact]
        public void MonthDayStringToDate_Mar31_Date()
        {
            var stringDate = "Mar, 31";
            var result = DateConversion.MonthDayStringToDate(stringDate);

            Assert.Equal(new DateTime(DateTime.Now.Year, 03, 31), result);
        }
        #endregion

        #region TimeAgoStringToDate

        [Theory]
        [InlineData("15d ago")]
        [InlineData("28d")]
        [InlineData("22h ago")]
        [InlineData("2h")]
        public void TimeAgoStringToDate_TimeAgo_ReturnsDate(string candidate)
        {
            var result = DateConversion.TimeAgoStringToDate(candidate);
            Assert.IsType<DateTime>(result);
        }

        [Theory]
        [InlineData("15 ago")]
        [InlineData("May 17")]
        [InlineData("Dec 21")]
        public void TimeAgoStringToDate_NotTimeAgo_ReturnsNull(string candidate)
        {
            var result = DateConversion.TimeAgoStringToDate(candidate);
            Assert.Null(result);
        }
        #endregion

        #region DayMonthYearToDate
        [Theory]
        [InlineData("20th July 2020")]
        [InlineData("Published: 18th July 2020")]
        [InlineData("18 June 2020")]
        [InlineData("Published: 1st June 2020")]
        [InlineData("Published: 2nd June 2020")]
        public void DayMonthYearToDate_DayMonthYear_ReturnsDate(string candidate)
        {
            var result = DateConversion.DayMonthYearToDate(candidate);
            Assert.IsType<DateTime>(result);
        }
        #endregion

    }
}
