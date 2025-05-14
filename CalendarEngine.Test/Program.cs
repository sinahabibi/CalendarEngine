using CalendarEngine.Enums;
using System.Globalization;


namespace CalendarEngine.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Calendar Engine Demonstration");
            Console.WriteLine("============================\n");

            // Test 1: Basic conversion between calendar types
            BasicCalendarConversions();

            // Test 2: Format demonstrations with different patterns
            FormatDemonstrations();

            // Test 3: Date comparisons across calendars
            DateComparisons();

            // Test 4: Using the instance-based approach with DI simulation
            //InstanceBasedApproach();

            // Test 5: Working with specific cultural dates
            SpecificCulturalDates();

            // Test 6: Error handling demonstrations
            ErrorHandlingExamples();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void BasicCalendarConversions()
        {
            Console.WriteLine("🔹 Basic Calendar Conversions");
            Console.WriteLine("----------------------------");

            // Current date in all calendars
            var now = DateTime.Now;
            Console.WriteLine($"Current date (Gregorian): {now:yyyy/MM/dd}");

            // Gregorian to Persian
            var persianDate = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(now, "yyyy/MM/dd");
            Console.WriteLine($"Current date (Persian): {persianDate}");

            // Gregorian to Hijri
            var hijriDate = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Hijri)
                .Format(now, "yyyy/MM/dd");
            Console.WriteLine($"Current date (Hijri): {hijriDate}");

            Console.WriteLine();
        }

        static void FormatDemonstrations()
        {
            Console.WriteLine("🔹 Format Demonstrations");
            Console.WriteLine("----------------------");

            var now = DateTime.Now;

            // Different format patterns for Persian
            Console.WriteLine("Persian date in different formats:");
            string[] persianFormats = {
                "yyyy/MM/dd",
                "yy/MM/dd",
                "yyyy-MM-dd",
                "d MMMM yyyy",
                "dddd, d MMMM yyyy"
            };

            foreach (var format in persianFormats)
            {
                var formatted = CalendarEngine
                    .From(CalendarType.Gregorian)
                    .To(CalendarType.Persian)
                    .Format(now, format);
                Console.WriteLine($"  {format,-20} → {formatted}");
            }

            // Different format patterns for Hijri
            Console.WriteLine("\nHijri date in different formats:");
            string[] hijriFormats = {
                "yyyy/MM/dd",
                "yy/MM/dd",
                "d MMMM yyyy",
                "dddd, d MMMM yyyy"
            };

            foreach (var format in hijriFormats)
            {
                var formatted = CalendarEngine
                    .From(CalendarType.Gregorian)
                    .To(CalendarType.Hijri)
                    .Format(now, format);
                Console.WriteLine($"  {format,-20} → {formatted}");
            }

            Console.WriteLine();
        }

        static void DateComparisons()
        {
            Console.WriteLine("🔹 Date Comparisons Across Calendars");
            Console.WriteLine("----------------------------------");

            // Define a specific Gregorian date
            var gregorianDate = new DateTime(2023, 3, 21); // Spring equinox
            Console.WriteLine($"Gregorian date: {gregorianDate:yyyy/MM/dd}");

            // Convert to Persian
            var persianDate = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(gregorianDate, "yyyy/MM/dd");
            Console.WriteLine($"Persian equivalent: {persianDate}");

            // Convert to Hijri
            var hijriDate = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Hijri)
                .Format(gregorianDate, "yyyy/MM/dd");
            Console.WriteLine($"Hijri equivalent: {hijriDate}");

            // Round-trip conversion: Persian → Gregorian → Persian
            PersianCalendar pc = new PersianCalendar();
            var persianDateTime = new DateTime(1402, 1, 1, pc);

            var gregorianEquivalent = CalendarEngine
                .From(CalendarType.Persian)
                .To(CalendarType.Gregorian)
                .Convert(persianDateTime);
            Console.WriteLine($"\nPersian date: 1402/01/01");
            Console.WriteLine($"Converted to Gregorian: {gregorianEquivalent:yyyy/MM/dd}");

            var backToPersian = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(gregorianEquivalent, "yyyy/MM/dd");
            Console.WriteLine($"Back to Persian: {backToPersian}");

            Console.WriteLine();
        }

        //static void InstanceBasedApproach()
        //{
        //    Console.WriteLine("🔹 Instance-Based Approach (DI Simulation)");
        //    Console.WriteLine("----------------------------------------");

        //    // Create a calendar engine instance (simulating DI)
        //    var converters = new Dictionary<CalendarType, ICalendarConverter>
        //    {
        //        { CalendarType.Gregorian, new CalendarEngine.Calendars.GregorianCalendar() },
        //        { CalendarType.Persian, new CalendarEngine.Calendars.PersianCalendar() },
        //        { CalendarType.Hijri, new CalendarEngine.Calendars.HijriCalendar() }
        //    };

        //    var calendarEngine = new CalendarEngine(converters);

        //    // Use the instance method
        //    var now = DateTime.Now;
        //    var persianDate = calendarEngine
        //        .CreateConverter(CalendarType.Gregorian)
        //        .To(CalendarType.Persian)
        //        .Format(now, "yyyy/MM/dd");

        //    Console.WriteLine($"Today in Persian (via instance method): {persianDate}");
        //    Console.WriteLine();
        //}

        static void SpecificCulturalDates()
        {
            Console.WriteLine("🔹 Working with Specific Cultural Dates");
            Console.WriteLine("------------------------------------");

            // Persian New Year (Nowruz) - typically March 21
            PersianCalendar pc = new PersianCalendar();
            var nowruz = new DateTime(1402, 1, 1, pc);

            Console.WriteLine($"Persian New Year (1402/01/01) in Gregorian: {nowruz:yyyy/MM/dd}");

            // Islamic New Year in Hijri
            HijriCalendar hc = new HijriCalendar();
            var islamicNewYear = new DateTime(1445, 1, 1, hc);

            Console.WriteLine($"Islamic New Year (1445/01/01) in Gregorian: {islamicNewYear:yyyy/MM/dd}");

            // Convert Christmas to Persian and Hijri
            var christmas = new DateTime(2023, 12, 25);
            var christmasInPersian = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(christmas, "yyyy/MM/dd");

            var christmasInHijri = CalendarEngine
                .From(CalendarType.Gregorian)
                .To(CalendarType.Hijri)
                .Format(christmas, "yyyy/MM/dd");

            Console.WriteLine($"Christmas 2023 (12/25) in Persian: {christmasInPersian}");
            Console.WriteLine($"Christmas 2023 (12/25) in Hijri: {christmasInHijri}");

            Console.WriteLine();
        }

        static void ErrorHandlingExamples()
        {
            Console.WriteLine("🔹 Error Handling Examples");
            Console.WriteLine("------------------------");

            try
            {
                // Attempt to use an invalid format
                var result = CalendarEngine
                    .From(CalendarType.Gregorian)
                    .To(CalendarType.Persian)
                    .Format(DateTime.Now, "invalid format");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format exception: {ex.Message}");
            }

            try
            {
                // Attempt to convert an out-of-range date
                var outOfRangeDate = new DateTime(10000, 1, 1);
                var result = CalendarEngine
                    .From(CalendarType.Gregorian)
                    .To(CalendarType.Persian)
                    .Format(outOfRangeDate, "yyyy/MM/dd");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Out of range date exception: {ex.Message}");
            }
        }
    }
}
