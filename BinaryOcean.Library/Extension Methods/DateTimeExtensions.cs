using System;
using System.Globalization;

namespace BinaryOcean.Library
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Adds the specified number of weeks to the value of the current System.DateTime.
        /// </summary>
        public static DateTime AddWeeks(this DateTime dateTime, double value)
        {
            return dateTime.AddDays(value * 7.0d);
        }

        /// <summary>
        /// Converts the value of the current System.DateTime to its equivalent short date and time string representation.
        /// </summary>
        public static string ToShortDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
        }

        /// <summary>
        /// Converts the value of the current System.DateTime to its equivalent long date and time string representation.
        /// </summary>
        public static string ToLongDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToLongDateString() + " " + dateTime.ToLongTimeString();
        }

        public static string ToShortDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToShortDateString();
        }

        public static string ToLongDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToLongDateString();
        }

        public static string ToShortTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToShortTimeString();
        }

        public static string ToLongTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToLongTimeString();
        }

        public static string ToShortDateTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToShortDateTimeString();
        }

        public static string ToLongDateTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            else
                return dateTime.Value.ToLongDateTimeString();
        }

        /// <summary>
        /// Get first day of month (no time)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Get last day of month (no time)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Get first date of a week (no time). Default start of week is Sunday.
        /// </summary>
        /// <param name="dateTime">The DateTime value used to define the week.</param>
        /// <returns>DateTime</returns>
        public static DateTime FirstDateOfWeek(this DateTime dateTime)
        {
            return dateTime.FirstDateOfWeek(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Get first date of a week (no time), specifying which DayOfWeek is the start of a week.
        /// </summary>
        /// <param name="dateTime">The DateTime value used to define the week.</param>
        /// <param name="weekStartsWith">The DayOfWeek value specifying the start day of the week.</param>
        /// <returns>DateTime</returns>
        public static DateTime FirstDateOfWeek(this DateTime dateTime, DayOfWeek weekStartsWith)
        {
            DateTime firstDateOfWeek = dateTime.AddDays(((int)weekStartsWith - (int)dateTime.DayOfWeek - 7) % 7);
            return new DateTime(firstDateOfWeek.Year, firstDateOfWeek.Month, firstDateOfWeek.Day);
        }

        /// <summary>
        /// Get name of month based on a DateTime.
        /// </summary>
        /// <param name="dateTime">The DateTime value used to define the week.</param>
        /// <returns>string</returns>
        public static string MonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }
    }
}