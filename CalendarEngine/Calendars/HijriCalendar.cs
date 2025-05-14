using CalendarEngine.Abstractions;
using System.Globalization;

namespace CalendarEngine.Calendars
{
    /// <summary>
    /// Implementation of the Hijri (Islamic) calendar system
    /// </summary>
    public class HijriCalendar : ICalendarConverter
    {
        private readonly System.Globalization.HijriCalendar _calendar = new();

        /// <summary>
        /// Converts a Hijri date to Gregorian
        /// </summary>
        public DateTime ConvertToGregorian(int year, int month, int day)
        {
            return _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Converts a Gregorian date to Hijri
        /// </summary>
        public (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime)
        {
            return (_calendar.GetYear(dateTime), _calendar.GetMonth(dateTime), _calendar.GetDayOfMonth(dateTime));
        }

        /// <summary>
        /// Formats a date using Hijri calendar conventions
        /// </summary>
        public string Format(DateTime dateTime, string format, CultureInfo? culture = null)
        {
            // Get Hijri components
            var year = _calendar.GetYear(dateTime);
            var month = _calendar.GetMonth(dateTime);
            var day = _calendar.GetDayOfMonth(dateTime);

            // Use Arabic culture if none specified
            culture ??= new CultureInfo("ar-SA");

            // Replace year, month, day tokens with Hijri values
            format = format.Replace("yyyy", year.ToString("0000", culture))
                           .Replace("yy", (year % 100).ToString("00", culture))
                           .Replace("MM", month.ToString("00", culture))
                           .Replace("M", month.ToString(culture))
                           .Replace("dd", day.ToString("00", culture))
                           .Replace("d", day.ToString(culture));

            return dateTime.ToString(format, culture);
        }
    }
}