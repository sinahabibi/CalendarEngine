using CalendarEngine.Enums;

namespace CalendarEngine.Abstractions
{
    /// <summary>
    /// Interface for selecting the target calendar type
    /// </summary>
    public interface ICalendarConverterSelector
    {
        /// <summary>
        /// Specifies the target calendar for conversion
        /// </summary>
        ICalendarFormatter To(CalendarType targetType);
    }
}