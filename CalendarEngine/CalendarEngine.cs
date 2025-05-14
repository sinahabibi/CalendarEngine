using CalendarEngine.Abstractions;
using CalendarEngine.Calendars;
using CalendarEngine.Enums;
using CalendarEngine.Implementation;

namespace CalendarEngine
{
    /// <summary>
    /// Main entry point for the Calendar Engine
    /// </summary>
    public class CalendarEngine : ICalendarEngine
    {
        private readonly Dictionary<CalendarType, ICalendarConverter> _converters;

        /// <summary>
        /// Initializes a new instance with the specified converters
        /// </summary>
        public CalendarEngine(Dictionary<CalendarType, ICalendarConverter> converters)
        {
            _converters = converters;
        }

        /// <summary>
        /// Static factory method that starts the fluent API chain
        /// </summary>
        public static ICalendarConverterSelector From(CalendarType sourceType)
        {
            return new CalendarConverterSelector(sourceType, GetDefaultConverters());
        }

        private static Dictionary<CalendarType, ICalendarConverter> GetDefaultConverters()
        {
            return new Dictionary<CalendarType, ICalendarConverter>
            {
                { CalendarType.Gregorian, new GregorianCalendar() },
                { CalendarType.Persian, new PersianCalendar() },
                { CalendarType.Hijri, new HijriCalendar() }
            };
        }

        /// <summary>
        /// Instance method that starts the fluent API chain (implements ICalendarEngine)
        /// </summary>
        public ICalendarConverterSelector CreateConverter(CalendarType sourceType)
        {
            return new CalendarConverterSelector(sourceType, _converters);
        }
    }
}