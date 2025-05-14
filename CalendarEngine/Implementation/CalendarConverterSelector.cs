using CalendarEngine.Abstractions;
using CalendarEngine.Enums;

namespace CalendarEngine.Implementation
{
    /// <summary>
    /// Implementation of the calendar converter selector
    /// </summary>
    internal class CalendarConverterSelector : ICalendarConverterSelector
    {
        private readonly CalendarType _sourceType;
        private readonly Dictionary<CalendarType, ICalendarConverter> _converters;

        /// <summary>
        /// Initializes a new selector with source calendar type and available converters
        /// </summary>
        public CalendarConverterSelector(CalendarType sourceType, Dictionary<CalendarType, ICalendarConverter> converters)
        {
            _sourceType = sourceType;
            _converters = converters;
        }

        /// <summary>
        /// Specifies the target calendar for conversion
        /// </summary>
        public ICalendarFormatter To(CalendarType targetType)
        {
            return new CalendarFormatter(_sourceType, targetType, _converters);
        }
    }
}