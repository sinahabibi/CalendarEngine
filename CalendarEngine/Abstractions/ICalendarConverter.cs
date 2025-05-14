using System.Globalization;

namespace CalendarEngine.Abstractions
{
    /// <summary>
    /// Interface defining operations for a specific calendar system
    /// </summary>
    public interface ICalendarConverter
    {
        /// <summary>
        /// Converts a date in this calendar to a Gregorian DateTime
        /// </summary>
        DateTime ConvertToGregorian(int year, int month, int day);

        /// <summary>
        /// Converts a Gregorian DateTime to a date in this calendar
        /// </summary>
        (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime);

        /// <summary>
        /// Formats a date using this calendar's conventions
        /// </summary>
        string Format(DateTime dateTime, string format, CultureInfo? culture = null);
    }
}