using CalendarEngine.Enums;

namespace CalendarEngine.Abstractions
{
    /// <summary>
    /// Interface defining the main entry point for the Calendar Engine
    /// </summary>
    public interface ICalendarEngine
    {
        /// <summary>
        /// Instance method that starts the fluent API chain
        /// </summary>
        ICalendarConverterSelector CreateConverter(CalendarType sourceType);
    }
}