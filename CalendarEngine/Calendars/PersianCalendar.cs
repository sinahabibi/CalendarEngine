using CalendarEngine.Abstractions;
using System.Globalization;

namespace CalendarEngine.Calendars
{
    /// <summary>
    /// Implementation of the Persian (Jalali) calendar system
    /// </summary>
    public class PersianCalendar : ICalendarConverter
    {
        private readonly System.Globalization.PersianCalendar _calendar = new();

        /// <summary>
        /// Converts a Persian date to Gregorian
        /// </summary>
        public DateTime ConvertToGregorian(int year, int month, int day)
        {
            return _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Converts a Gregorian date to Persian
        /// </summary>
        public (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime)
        {
            return (_calendar.GetYear(dateTime), _calendar.GetMonth(dateTime), _calendar.GetDayOfMonth(dateTime));
        }

        /// <summary>
        /// Formats a date using Persian calendar conventions
        /// </summary>
        public string Format(DateTime dateTime, string format, CultureInfo? culture = null)
        {
            // Get Persian components
            var year = _calendar.GetYear(dateTime);
            var month = _calendar.GetMonth(dateTime);
            var day = _calendar.GetDayOfMonth(dateTime);
            var dayOfWeek = _calendar.GetDayOfWeek(dateTime);

            // Custom formatting with Persian month names and numerals
            culture ??= new CultureInfo("fa-IR");

            // Replace year, month, day tokens with Persian values
            format = format.Replace("yyyy", year.ToString("0000", culture))
                           .Replace("yy", (year % 100).ToString("00", culture))
                           .Replace("MM", month.ToString("00", culture))
                           .Replace("M", month.ToString(culture))
                           .Replace("dd", day.ToString("00", culture))
                           .Replace("d", day.ToString(culture));

            // Handle remaining tokens from the original DateTime
            return dateTime.ToString(format, culture);
        }
    }
}