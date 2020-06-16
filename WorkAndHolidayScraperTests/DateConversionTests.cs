using System;
using WorkAndHolidayScraper.Models;
using Xunit;

namespace WorkAndHolidayScraperTests
{
    public class DateConversionTests
    {
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

        #region DaysAgoStringToDate

        [Theory]
        [InlineData("15d ago")]
        [InlineData("28d")]
        [InlineData("22h ago")]
        [InlineData("2h")]
        public void DaysAgoStringToDate_DaysAgo_ReturnsDate(string candidate)
        {
            var result = DateConversion.TimeAgoStringToDate(candidate);
            Assert.IsType<DateTime>(result);
        }

        [Theory]
        [InlineData("15 ago")]
        [InlineData("May 17")]
        [InlineData("Dec 21")]
        public void DaysAgoStringToDate_NotDaysAgo_ReturnsNull(string candidate)
        {
            var result = DateConversion.TimeAgoStringToDate(candidate);
            Assert.Null(result);
        }
        #endregion

    }
}
