namespace CalendarEngine.Abstractions
{
    /// <summary>
    /// Interface for formatting dates between calendar systems
    /// </summary>
    public interface ICalendarFormatter
    {
        /// <summary>
        /// Formats a date according to the specified format string
        /// </summary>
        string Format(DateTime dateTime, string format);

        /// <summary>
        /// Converts a date from source calendar to target calendar
        /// </summary>
        DateTime Convert(DateTime dateTime);
    }
}