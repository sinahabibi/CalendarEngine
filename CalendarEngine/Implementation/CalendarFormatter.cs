using CalendarEngine.Abstractions;
using CalendarEngine.Enums;

namespace CalendarEngine.Implementation
{
    /// <summary>
    /// Implementation of the calendar formatter
    /// </summary>
    internal class CalendarFormatter : ICalendarFormatter
    {
        private readonly CalendarType _sourceType;
        private readonly CalendarType _targetType;
        private readonly Dictionary<CalendarType, ICalendarConverter> _converters;

        /// <summary>
        /// Initializes a new formatter with source and target calendar types
        /// </summary>
        public CalendarFormatter(CalendarType sourceType, CalendarType targetType,
            Dictionary<CalendarType, ICalendarConverter> converters)
        {
            _sourceType = sourceType;
            _targetType = targetType;
            _converters = converters;
        }

        /// <summary>
        /// Formats a date according to the specified format string
        /// </summary>
        public string Format(DateTime dateTime, string format)
        {
            if (!_converters.TryGetValue(_sourceType, out var sourceConverter) ||
                !_converters.TryGetValue(_targetType, out var targetConverter))
            {
                throw new ArgumentException("Calendar type not supported");
            }

            // If source and target are the same, just format directly
            if (_sourceType == _targetType)
                return targetConverter.Format(dateTime, format);

            // Otherwise convert first
            var convertedDateTime = Convert(dateTime);
            return targetConverter.Format(convertedDateTime, format);
        }

        /// <summary>
        /// Converts a date from source calendar to target calendar
        /// </summary>
        public DateTime Convert(DateTime dateTime)
        {
            if (!_converters.TryGetValue(_sourceType, out var sourceConverter) ||
                !_converters.TryGetValue(_targetType, out var targetConverter))
            {
                throw new ArgumentException("Calendar type not supported");
            }

            // If source and target are the same, no conversion needed
            if (_sourceType == _targetType)
                return dateTime;

            // First convert source date to its components
            var sourceDateComponents = sourceConverter.ConvertFromGregorian(dateTime);

            // Then create a gregorian date from those components
            var gregorianDate = sourceConverter.ConvertToGregorian(
                sourceDateComponents.Year,
                sourceDateComponents.Month,
                sourceDateComponents.Day);

            // Finally, get the target date components
            var targetDateComponents = targetConverter.ConvertFromGregorian(gregorianDate);

            // And create a new gregorian date representing the target date
            return targetConverter.ConvertToGregorian(
                targetDateComponents.Year,
                targetDateComponents.Month,
                targetDateComponents.Day);
        }
    }
}