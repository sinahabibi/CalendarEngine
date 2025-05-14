using CalendarEngine.Abstractions;
using System.Globalization;

namespace CalendarEngine.Calendars
{
    /// <summary>
    /// Implementation of the Gregorian calendar system
    /// </summary>
    public class GregorianCalendar : ICalendarConverter
    {
        /// <summary>
        /// Converts a Gregorian date to a Gregorian DateTime (identity operation)
        /// </summary>
        public DateTime ConvertToGregorian(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Gets year, month, and day components from a Gregorian DateTime
        /// </summary>
        public (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime)
        {
            return (dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// Formats a Gregorian date with the specified format
        /// </summary>
        public string Format(DateTime dateTime, string format, CultureInfo? culture = null)
        {
            return dateTime.ToString(format, culture ?? CultureInfo.InvariantCulture);
        }
    }
}