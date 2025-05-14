using CalendarEngine.Abstractions;
using CalendarEngine.Calendars;
using CalendarEngine.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarEngine.Extensions
{
    /// <summary>
    /// Extension methods for registering CalendarEngine with DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds CalendarEngine services to the DI container
        /// </summary>
        public static IServiceCollection AddCalendarEngine(this IServiceCollection services)
        {
            // Register calendar converters
            services.AddSingleton<ICalendarConverter, GregorianCalendar>();
            services.AddSingleton<ICalendarConverter, PersianCalendar>();
            services.AddSingleton<ICalendarConverter, HijriCalendar>();

            // Register the calendar engine with all converters
            services.AddSingleton<CalendarEngine>(sp =>
            {
                var converters = new Dictionary<CalendarType, ICalendarConverter>
                {
                    { CalendarType.Gregorian, sp.GetRequiredService<GregorianCalendar>() },
                    { CalendarType.Persian, sp.GetRequiredService<PersianCalendar>() },
                    { CalendarType.Hijri, sp.GetRequiredService<HijriCalendar>() }
                };

                return new CalendarEngine(converters);
            });

            return services;
        }
    }
}