# CalendarEngine - Comprehensive Documentation

> **Language Selection | انتخاب زبان:**
>
> - [English](#english)
> - [فارسی (Persian)](#persian)

<a name="english"></a>
# CalendarEngine Library - Complete Documentation

## Table of Contents

- [Introduction](#introduction)
- [Installation and Setup](#installation-and-setup)
- [Architecture and Project Structure](#architecture-and-project-structure)
- [Date Conversion Types](#date-conversion-types)
- [Date Formatting](#date-formatting)
- [Working with Cultural Dates](#working-with-cultural-dates)
- [Dependency Injection in Projects](#dependency-injection-in-projects)
- [Extending the Library](#extending-the-library)
- [Troubleshooting Common Issues](#troubleshooting-common-issues)

## Introduction

**CalendarEngine** is a comprehensive solution for converting dates between different calendar systems. This library is designed using the Fluent API pattern, making it very simple and readable to use.

**Supported Calendar Systems:**
- Gregorian Calendar
- Persian (Jalali) Calendar
- Hijri (Islamic) Calendar

**Main Features:**
- Easy conversion between calendars
- Advanced formatting with support for different cultures
- Dependency Injection (DI) support
- Extensible design

## Installation and Setup

### Method 1: Installation via NuGet Package Manager

1. Right-click on your project in Visual Studio
2. Select `Manage NuGet Packages`
3. In the Browse tab, search for `CalendarEngine`
4. Click on the result and press the Install button

### Method 2: Installation via Command Line

**Using .NET CLI:**

```sh
dotnet add package CalendarEngine
```

**Using Package Manager Console:**

```powershell
Install-Package CalendarEngine
```

### Method 3: Manual Library Installation

1. Download the project from the [GitHub page](https://github.com/SinaHabibi/calendarengine)
2. Open it in Visual Studio
3. Build the project
4. Add the generated DLL to your project

### Adding Required Usings

After installation, add these usings to your class:

```csharp
using CalendarEngine;
using CalendarEngine.Enums;
// For dependency injection if needed:
using CalendarEngine.Extensions;
```

## Architecture and Project Structure

CalendarEngine is designed based on SOLID principles and uses clean architecture:

### Main Namespaces

1. **`CalendarEngine`**: Main class and entry point to the library
2. **`CalendarEngine.Enums`**: Calendar type definitions
3. **`CalendarEngine.Abstractions`**: Interfaces and contracts
4. **`CalendarEngine.Calendars`**: Implementations of different calendars
5. **`CalendarEngine.Implementation`**: Internal implementations
6. **`CalendarEngine.Extensions`**: Extension methods for dependency injection

### Main Architecture Components

1. **`ICalendarConverter`**: Main interface for defining conversions between calendars
2. **`ICalendarConverterSelector`**: Interface for selecting the target calendar
3. **`ICalendarFormatter`**: Interface for date formatting
4. **`CalendarType`**: Enum data type for determining calendar types

### Fluent API Pattern

The Fluent API structure is as follows:

```
CalendarEngine.From(sourceCalendarType).To(targetCalendarType).Format(dateTime, formatString)
```

Or for date conversion without formatting:

```
CalendarEngine.From(sourceCalendarType).To(targetCalendarType).Convert(dateTime)
```

## Date Conversion Types

### Simple Conversion Between Calendars

#### From Gregorian to Persian

```csharp
// Convert today's date from Gregorian to Persian
DateTime today = DateTime.Now;
string persianDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(today, "yyyy/MM/dd");

Console.WriteLine($"Today's date in Persian: {persianDate}");
```

#### From Gregorian to Hijri

```csharp
// Convert today's date from Gregorian to Hijri
DateTime today = DateTime.Now;
string hijriDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(today, "yyyy/MM/dd");

Console.WriteLine($"Today's date in Hijri: {hijriDate}");
```

#### From Persian to Gregorian

```csharp
// Create a Persian date using .NET's PersianCalendar class
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 10, 25, pc);

// Convert to Gregorian
DateTime gregorianDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDate);

Console.WriteLine($"Persian date 1402/10/25 in Gregorian: {gregorianDate:yyyy/MM/dd}");
```

### Round-trip Conversion

```csharp
// Convert from Persian to Gregorian and back to Persian
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 1, 1, pc);

DateTime gregorianDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDate);

string backToPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(gregorianDate, "yyyy/MM/dd");

Console.WriteLine($"Original date: 1402/01/01");
Console.WriteLine($"Converted to Gregorian: {gregorianDate:yyyy/MM/dd}");
Console.WriteLine($"Back to Persian: {backToPersian}");
```

### Direct Conversion Between Persian and Hijri

```csharp
// Create a Persian date
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 1, 1, pc);

// Direct conversion from Persian to Hijri
string hijriDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Hijri)
    .Format(persianDate, "yyyy/MM/dd");

Console.WriteLine($"Persian date 1402/01/01 in Hijri: {hijriDate}");
```

## Date Formatting

### Common Formatting Patterns

- `yyyy`: Four-digit year
- `yy`: Two-digit year
- `MM`: Two-digit month
- `M`: Month without leading zero
- `dd`: Two-digit day
- `d`: Day without leading zero
- `dddd`: Full name of the day of the week
- `ddd`: Abbreviated day of the week
- `MMMM`: Full month name
- `MMM`: Abbreviated month name

### Various Formatting Examples

```csharp
DateTime now = DateTime.Now;
string[] formats = {
    "yyyy/MM/dd",        // Example: 2024/01/15
    "yy/MM/dd",          // Example: 24/01/15
    "yyyy-MM-dd",        // Example: 2024-01-15
    "d MMMM yyyy",       // Example: 15 January 2024
    "dddd, d MMMM yyyy", // Example: Monday, 15 January 2024
    "yyyy/MM/dd HH:mm",  // Example: 2024/01/15 14:30
    "HH:mm:ss yyyy/MM/dd" // Example: 14:30:45 2024/01/15
};

foreach (var format in formats)
{
    string formatted = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(now, format);
    
    Console.WriteLine($"{format,-20} → {formatted}");
}
```

### Using Specific Cultures

The Format method in ICalendarConverter implementations has an optional parameter of type CultureInfo that you can use:

```csharp
// Direct use of Persian calendar implementation with Persian culture
var persianCalendar = new CalendarEngine.Calendars.PersianCalendar();
string formatted = persianCalendar.Format(
    DateTime.Now, 
    "dddd d MMMM yyyy", 
    new System.Globalization.CultureInfo("fa-IR")
);

Console.WriteLine(formatted);
```

## Working with Cultural Dates

### Important Dates in Persian Calendar

```csharp
// Nowruz (Persian New Year)
var pc = new System.Globalization.PersianCalendar();
var nowruz = new DateTime(1402, 1, 1, pc);

// Display Nowruz in Gregorian
Console.WriteLine($"Nowruz 1402 in Gregorian: {nowruz:yyyy/MM/dd}");

// Yalda Night (December 21)
var yalda = new DateTime(1402, 9, 30, pc);
Console.WriteLine($"Yalda Night 1402 in Gregorian: {yalda:yyyy/MM/dd}");
```

### Important Dates in Hijri Calendar

```csharp
// Islamic New Year
var hc = new System.Globalization.HijriCalendar();
var islamicNewYear = new DateTime(1445, 1, 1, hc);

// Convert to Gregorian and Persian
Console.WriteLine($"Islamic New Year 1445 in Gregorian: {islamicNewYear:yyyy/MM/dd}");

string persianDate = CalendarEngine
    .From(CalendarType.Hijri)
    .To(CalendarType.Persian)
    .Format(islamicNewYear, "yyyy/MM/dd");
    
Console.WriteLine($"Islamic New Year 1445 in Persian: {persianDate}");

// Ramadan month
var ramadan = new DateTime(1445, 9, 1, hc);
Console.WriteLine($"First day of Ramadan 1445 in Gregorian: {ramadan:yyyy/MM/dd}");
```

### Converting Gregorian Holidays to Other Calendars

```csharp
// Christmas
var christmas = new DateTime(2023, 12, 25);

// Christmas to Persian
string christmasInPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(christmas, "yyyy/MM/dd");
    
Console.WriteLine($"Christmas 2023 in Persian: {christmasInPersian}");

// Christmas to Hijri
string christmasInHijri = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(christmas, "yyyy/MM/dd");
    
Console.WriteLine($"Christmas 2023 in Hijri: {christmasInHijri}");
```

## Dependency Injection in Projects

### Setting up Services in ASP.NET Core

```csharp
// In Program.cs or Startup.cs
using CalendarEngine.Extensions;

// ...

public void ConfigureServices(IServiceCollection services)
{
    // Add CalendarEngine to DI services
    services.AddCalendarEngine();
    
    // Other services...
    services.AddControllers();
    // ...
}
```

### Using in a Controller or Service

```csharp
// Example usage in an ASP.NET Core controller
using CalendarEngine;
using CalendarEngine.Abstractions;
using CalendarEngine.Enums;

public class DateController : ControllerBase
{
    private readonly ICalendarEngine _calendarEngine;
    
    // Dependency injection in constructor
    public DateController(ICalendarEngine calendarEngine)
    {
        _calendarEngine = calendarEngine;
    }
    
    [HttpGet("today/persian")]
    public IActionResult GetTodayPersian()
    {
        string persianDate = _calendarEngine
            .CreateConverter(CalendarType.Gregorian)
            .To(CalendarType.Persian)
            .Format(DateTime.Now, "yyyy/MM/dd");
            
        return Ok(new { date = persianDate });
    }
    
    [HttpGet("convert")]
    public IActionResult ConvertDate(DateTime date, string format)
    {
        try
        {
            string persianDate = _calendarEngine
                .CreateConverter(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(date, format ?? "yyyy/MM/dd");
                
            string hijriDate = _calendarEngine
                .CreateConverter(CalendarType.Gregorian)
                .To(CalendarType.Hijri)
                .Format(date, format ?? "yyyy/MM/dd");
                
            return Ok(new { 
                gregorian = date.ToString("yyyy/MM/dd"),
                persian = persianDate,
                hijri = hijriDate
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
```

## Extending the Library

### Adding a New Calendar

To add a new calendar to the library, follow these steps:

#### 1. Add a New Calendar Type to the Enum

```csharp
// In CalendarType.cs
namespace CalendarEngine.Enums
{
    public enum CalendarType
    {
        Gregorian,
        Persian,
        Hijri,
        // Add new calendar
        Hebrew
    }
}
```

#### 2. Implement the New Calendar Class

```csharp
// Example implementation of the Hebrew calendar
using System;
using System.Globalization;
using CalendarEngine.Abstractions;

namespace CalendarEngine.Calendars
{
    public class HebrewCalendar : ICalendarConverter
    {
        private readonly System.Globalization.HebrewCalendar _calendar = new();

        public DateTime ConvertToGregorian(int year, int month, int day)
        {
            return _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime)
        {
            return (_calendar.GetYear(dateTime), _calendar.GetMonth(dateTime), _calendar.GetDayOfMonth(dateTime));
        }

        public string Format(DateTime dateTime, string format, CultureInfo? culture = null)
        {
            // Get Hebrew components
            var year = _calendar.GetYear(dateTime);
            var month = _calendar.GetMonth(dateTime);
            var day = _calendar.GetDayOfMonth(dateTime);
            
            // Use Hebrew culture if none specified
            culture ??= new CultureInfo("he-IL");
            
            // Replace tokens with Hebrew values
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
```

#### 3. Register the New Calendar in the Main Class

```csharp
// In CalendarEngine.cs
private static Dictionary<CalendarType, ICalendarConverter> GetDefaultConverters()
{
    return new Dictionary<CalendarType, ICalendarConverter>
    {
        { CalendarType.Gregorian, new GregorianCalendar() },
        { CalendarType.Persian, new PersianCalendar() },
        { CalendarType.Hijri, new HijriCalendar() },
        // Register new calendar
        { CalendarType.Hebrew, new HebrewCalendar() }
    };
}
```

#### 4. Register the New Calendar in Dependency Injection

```csharp
// In ServiceCollectionExtensions.cs
public static IServiceCollection AddCalendarEngine(this IServiceCollection services)
{
    // Register calendar converters
    services.AddSingleton<ICalendarConverter, GregorianCalendar>();
    services.AddSingleton<ICalendarConverter, PersianCalendar>();
    services.AddSingleton<ICalendarConverter, HijriCalendar>();
    // Register new calendar
    services.AddSingleton<ICalendarConverter, HebrewCalendar>();
    
    // Register the calendar engine with all converters
    services.AddSingleton<CalendarEngine>(sp =>
    {
        var converters = new Dictionary<CalendarType, ICalendarConverter>
        {
            { CalendarType.Gregorian, sp.GetRequiredService<GregorianCalendar>() },
            { CalendarType.Persian, sp.GetRequiredService<PersianCalendar>() },
            { CalendarType.Hijri, sp.GetRequiredService<HijriCalendar>() },
            // Register new calendar
            { CalendarType.Hebrew, sp.GetRequiredService<HebrewCalendar>() }
        };
        
        return new CalendarEngine(converters);
    });
    
    return services;
}
```

### Improving Existing Formatting

If you want to improve the formatting of an existing calendar, you can extend the relevant class:

```csharp
// Example of extending the Persian calendar with improved formatting
public class EnhancedPersianCalendar : PersianCalendar
{
    // Persian month names
    private readonly string[] _monthNames = new[]
    {
        "Farvardin", "Ordibehesht", "Khordad", "Tir", "Mordad", "Shahrivar",
        "Mehr", "Aban", "Azar", "Dey", "Bahman", "Esfand"
    };
    
    // Persian day names
    private readonly string[] _dayNames = new[]
    {
        "Yekshanbe", "Doshanbe", "Seshanbe", "Chaharshanbeh", "Panjshanbeh", "Jomeh", "Shanbe"
    };
    
    // Override Format method for better formatting support
    public override string Format(DateTime dateTime, string format, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo("fa-IR");
        var components = ConvertFromGregorian(dateTime);
        
        // Replace month and day names
        if (format.Contains("MMMM"))
        {
            format = format.Replace("MMMM", _monthNames[components.Month - 1]);
        }
        else if (format.Contains("MMM"))
        {
            format = format.Replace("MMM", _monthNames[components.Month - 1].Substring(0, 3));
        }
        
        if (format.Contains("dddd"))
        {
            int dayOfWeek = (int)new System.Globalization.PersianCalendar().GetDayOfWeek(dateTime);
            format = format.Replace("dddd", _dayNames[dayOfWeek]);
        }
        else if (format.Contains("ddd"))
        {
            int dayOfWeek = (int)new System.Globalization.PersianCalendar().GetDayOfWeek(dateTime);
            format = format.Replace("ddd", _dayNames[dayOfWeek].Substring(0, 3));
        }
        
        // Other formatting using base method
        return base.Format(dateTime, format, culture);
    }
}
```

## Troubleshooting Common Issues

### FormatException Error

**Cause:** Using an invalid formatting pattern

**Solution:**
```csharp
try
{
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(DateTime.Now, "invalid-format");
}
catch (FormatException ex)
{
    Console.WriteLine($"Format error: {ex.Message}");
    // Use a valid format
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(DateTime.Now, "yyyy/MM/dd");
}
```

### Date Out of Range Error

**Cause:** Using a date that is outside the range of one of the calendars

**Solution:**
```csharp
try
{
    var outOfRangeDate = new DateTime(10000, 1, 1);
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(outOfRangeDate, "yyyy/MM/dd");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Date out of range error: {ex.Message}");
    // Use a valid date
}
```

### Unsupported Calendar Error

**Cause:** Using a calendar type that is not implemented

**Solution:**
```csharp
try
{
    // Assume CalendarType.Hebrew is not yet implemented
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To((CalendarType)99) // Invalid calendar
        .Format(DateTime.Now, "yyyy/MM/dd");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid calendar error: {ex.Message}");
    // Use a valid calendar
}
```

---

<a name="persian"></a>
<div dir="rtl">

# راهنمای کامل کتابخانه CalendarEngine

## فهرست مطالب

- [مقدمه و معرفی کلی](#مقدمه-و-معرفی-کلی)
- [نصب و راه‌اندازی](#نصب-و-راه‌اندازی)
- [معماری و ساختار پروژه](#معماری-و-ساختار-پروژه)
- [انواع تبدیلات تاریخ](#انواع-تبدیلات-تاریخ)
- [قالب‌بندی تاریخ](#قالب‌بندی-تاریخ)
- [کار با تاریخ‌های فرهنگی](#کار-با-تاریخ‌های-فرهنگی)
- [تزریق وابستگی در پروژه‌ها](#تزریق-وابستگی-در-پروژه‌ها)
- [گسترش کتابخانه](#گسترش-کتابخانه)
- [عیب‌یابی مشکلات رایج](#عیب‌یابی-مشکلات-رایج)

## مقدمه و معرفی کلی

**کتابخانه CalendarEngine** یک راه‌حل جامع برای تبدیل تاریخ بین سیستم‌های تقویمی مختلف است. این کتابخانه با استفاده از الگوی Fluent API طراحی شده که استفاده از آن را بسیار ساده و خوانا می‌کند.

**سیستم‌های تقویمی پشتیبانی شده:**
- تقویم میلادی (Gregorian)
- تقویم شمسی (Persian/Jalali)
- تقویم هجری قمری (Hijri/Islamic)

**ویژگی‌های اصلی:**
- تبدیل آسان بین تقویم‌ها
- قالب‌بندی پیشرفته با پشتیبانی از فرهنگ‌های مختلف
- پشتیبانی از تزریق وابستگی (DI)
- طراحی قابل گسترش

## نصب و راه‌اندازی

### روش 1: نصب از طریق NuGet Package Manager

1. در Visual Studio روی پروژه خود راست کلیک کنید
2. گزینه `Manage NuGet Packages` را انتخاب کنید
3. در تب Browse، عبارت `CalendarEngine` را جستجو کنید
4. روی نتیجه کلیک کرده و دکمه Install را بزنید

### روش 2: نصب از طریق خط فرمان

**با استفاده از .NET CLI:**

```sh
dotnet add package CalendarEngine
```

**با استفاده از Package Manager Console:**

```powershell
Install-Package CalendarEngine
```

### روش 3: نصب دستی کتابخانه

1. از [صفحه GitHub](https://github.com/SinaHabibi/calendarengine) پروژه را دانلود کنید
2. آن را در Visual Studio باز کنید
3. پروژه را build کنید
4. DLL تولید شده را به پروژه خود اضافه کنید

### اضافه کردن using های لازم

بعد از نصب، using های زیر را به کلاس خود اضافه کنید:

```csharp
using CalendarEngine;
using CalendarEngine.Enums;
// در صورت نیاز به تزریق وابستگی:
using CalendarEngine.Extensions;
```

## معماری و ساختار پروژه

کتابخانه CalendarEngine بر اساس اصول SOLID طراحی شده و از معماری تمیز استفاده می‌کند:

### فضاهای نام اصلی

1. **`CalendarEngine`**: کلاس اصلی و نقطه ورود به کتابخانه
2. **`CalendarEngine.Enums`**: تعاریف انواع تقویم‌ها
3. **`CalendarEngine.Abstractions`**: اینترفیس‌ها و قراردادها
4. **`CalendarEngine.Calendars`**: پیاده‌سازی‌های تقویم‌های مختلف
5. **`CalendarEngine.Implementation`**: پیاده‌سازی‌های داخلی
6. **`CalendarEngine.Extensions`**: متدهای الحاقی برای تزریق وابستگی

### اجزای اصلی معماری

1. **`ICalendarConverter`**: اینترفیس اصلی برای تعریف تبدیلات بین تقویم‌ها
2. **`ICalendarConverterSelector`**: اینترفیس انتخاب کننده تقویم مقصد
3. **`ICalendarFormatter`**: اینترفیس قالب‌بندی تاریخ
4. **`CalendarType`**: نوع داده enum برای تعیین انواع تقویم

### الگوی Fluent API

ساختار Fluent API به این صورت است:

```
CalendarEngine.From(sourceCalendarType).To(targetCalendarType).Format(dateTime, formatString)
```

یا برای تبدیل تاریخ بدون قالب‌بندی:

```
CalendarEngine.From(sourceCalendarType).To(targetCalendarType).Convert(dateTime)
```

## انواع تبدیلات تاریخ

### تبدیل ساده بین تقویم‌ها

#### از تقویم میلادی به شمسی

```csharp
// تبدیل تاریخ امروز از میلادی به شمسی
DateTime today = DateTime.Now;
string persianDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(today, "yyyy/MM/dd");

Console.WriteLine($"تاریخ امروز به شمسی: {persianDate}");
```

#### از تقویم میلادی به هجری قمری

```csharp
// تبدیل تاریخ امروز از میلادی به هجری قمری
DateTime today = DateTime.Now;
string hijriDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(today, "yyyy/MM/dd");

Console.WriteLine($"تاریخ امروز به هجری قمری: {hijriDate}");
```

#### از تقویم شمسی به میلادی

```csharp
// ایجاد یک تاریخ شمسی با استفاده از کلاس PersianCalendar داخلی .NET
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 10, 25, pc);

// تبدیل به میلادی
DateTime gregorianDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDate);

Console.WriteLine($"تاریخ شمسی 1402/10/25 به میلادی: {gregorianDate:yyyy/MM/dd}");
```

### تبدیل رفت و برگشتی

```csharp
// تبدیل از شمسی به میلادی و برگشت به شمسی
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 1, 1, pc);

DateTime gregorianDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDate);

string backToPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(gregorianDate, "yyyy/MM/dd");

Console.WriteLine($"تاریخ اصلی: 1402/01/01");
Console.WriteLine($"تبدیل به میلادی: {gregorianDate:yyyy/MM/dd}");
Console.WriteLine($"برگشت به شمسی: {backToPersian}");
```

### تبدیل مستقیم بین شمسی و هجری

```csharp
// ایجاد یک تاریخ شمسی
System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
DateTime persianDate = new DateTime(1402, 1, 1, pc);

// تبدیل مستقیم از شمسی به هجری
string hijriDate = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Hijri)
    .Format(persianDate, "yyyy/MM/dd");

Console.WriteLine($"تاریخ شمسی 1402/01/01 به هجری قمری: {hijriDate}");
```

## قالب‌بندی تاریخ

### الگوهای قالب‌بندی رایج

- `yyyy`: سال چهار رقمی
- `yy`: سال دو رقمی
- `MM`: ماه دو رقمی
- `M`: ماه بدون صفر ابتدایی
- `dd`: روز دو رقمی
- `d`: روز بدون صفر ابتدایی
- `dddd`: نام کامل روز هفته
- `ddd`: نام کوتاه روز هفته
- `MMMM`: نام کامل ماه
- `MMM`: نام کوتاه ماه

### نمونه‌های مختلف قالب‌بندی

```csharp
DateTime now = DateTime.Now;
string[] formats = {
    "yyyy/MM/dd",        // مثال: 1402/01/01
    "yy/MM/dd",          // مثال: 02/01/01
    "yyyy-MM-dd",        // مثال: 1402-01-01
    "d MMMM yyyy",       // مثال: 1 فروردین 1402
    "dddd, d MMMM yyyy", // مثال: چهارشنبه، 1 فروردین 1402
    "yyyy/MM/dd HH:mm",  // مثال: 1402/01/01 14:30
    "HH:mm:ss yyyy/MM/dd" // مثال: 14:30:45 1402/01/01
};

foreach (var format in formats)
{
    string formatted = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(now, format);
    
    Console.WriteLine($"{format,-20} → {formatted}");
}
```

### استفاده از فرهنگ‌های خاص (Cultures)

متد Format در پیاده‌سازی‌های ICalendarConverter یک پارامتر اختیاری از نوع CultureInfo دارد که می‌توانید از آن استفاده کنید:

```csharp
// استفاده مستقیم از پیاده‌سازی تقویم شمسی با فرهنگ فارسی
var persianCalendar = new CalendarEngine.Calendars.PersianCalendar();
string formatted = persianCalendar.Format(
    DateTime.Now, 
    "dddd d MMMM yyyy", 
    new System.Globalization.CultureInfo("fa-IR")
);

Console.WriteLine(formatted);
```

## کار با تاریخ‌های فرهنگی

### تاریخ‌های مهم در تقویم شمسی

```csharp
// نوروز (سال نو شمسی)
var pc = new System.Globalization.PersianCalendar();
var nowruz = new DateTime(1402, 1, 1, pc);

// نمایش نوروز به میلادی
Console.WriteLine($"نوروز 1402 به میلادی: {nowruz:yyyy/MM/dd}");

// شب یلدا (30 آذر)
var yalda = new DateTime(1402, 9, 30, pc);
Console.WriteLine($"شب یلدا 1402 به میلادی: {yalda:yyyy/MM/dd}");
```

### تاریخ‌های مهم در تقویم هجری قمری

```csharp
// سال نو هجری
var hc = new System.Globalization.HijriCalendar();
var islamicNewYear = new DateTime(1445, 1, 1, hc);

// تبدیل به میلادی و شمسی
Console.WriteLine($"سال نو هجری 1445 به میلادی: {islamicNewYear:yyyy/MM/dd}");

string persianDate = CalendarEngine
    .From(CalendarType.Hijri)
    .To(CalendarType.Persian)
    .Format(islamicNewYear, "yyyy/MM/dd");
    
Console.WriteLine($"سال نو هجری 1445 به شمسی: {persianDate}");

// ماه رمضان
var ramadan = new DateTime(1445, 9, 1, hc);
Console.WriteLine($"اول رمضان 1445 به میلادی: {ramadan:yyyy/MM/dd}");
```

### تبدیل مناسبت‌های میلادی به تقویم‌های دیگر

```csharp
// کریسمس
var christmas = new DateTime(2023, 12, 25);

// کریسمس به شمسی
string christmasInPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(christmas, "yyyy/MM/dd");
    
Console.WriteLine($"کریسمس 2023 به شمسی: {christmasInPersian}");

// کریسمس به هجری
string christmasInHijri = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(christmas, "yyyy/MM/dd");
    
Console.WriteLine($"کریسمس 2023 به هجری قمری: {christmasInHijri}");
```

## تزریق وابستگی در پروژه‌ها

### تنظیم سرویس‌ها در ASP.NET Core

```csharp
// در فایل Program.cs یا Startup.cs
using CalendarEngine.Extensions;

// ...

public void ConfigureServices(IServiceCollection services)
{
    // اضافه کردن CalendarEngine به سرویس‌های DI
    services.AddCalendarEngine();
    
    // سایر سرویس‌ها...
    services.AddControllers();
    // ...
}
```

### استفاده در کنترلر یا سرویس

```csharp
// نمونه استفاده در کنترلر ASP.NET Core
using CalendarEngine;
using CalendarEngine.Abstractions;
using CalendarEngine.Enums;

public class DateController : ControllerBase
{
    private readonly ICalendarEngine _calendarEngine;
    
    // تزریق وابستگی در سازنده
    public DateController(ICalendarEngine calendarEngine)
    {
        _calendarEngine = calendarEngine;
    }
    
    [HttpGet("today/persian")]
    public IActionResult GetTodayPersian()
    {
        string persianDate = _calendarEngine
            .CreateConverter(CalendarType.Gregorian)
            .To(CalendarType.Persian)
            .Format(DateTime.Now, "yyyy/MM/dd");
            
        return Ok(new { date = persianDate });
    }
    
    [HttpGet("convert")]
    public IActionResult ConvertDate(DateTime date, string format)
    {
        try
        {
            string persianDate = _calendarEngine
                .CreateConverter(CalendarType.Gregorian)
                .To(CalendarType.Persian)
                .Format(date, format ?? "yyyy/MM/dd");
                
            string hijriDate = _calendarEngine
                .CreateConverter(CalendarType.Gregorian)
                .To(CalendarType.Hijri)
                .Format(date, format ?? "yyyy/MM/dd");
                
            return Ok(new { 
                gregorian = date.ToString("yyyy/MM/dd"),
                persian = persianDate,
                hijri = hijriDate
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
```

## گسترش کتابخانه

### افزودن تقویم جدید

برای افزودن یک تقویم جدید به کتابخانه، این مراحل را دنبال کنید:

#### 1. افزودن نوع تقویم جدید به enum

```csharp
// در فایل CalendarType.cs
namespace CalendarEngine.Enums
{
    public enum CalendarType
    {
        Gregorian,
        Persian,
        Hijri,
        // افزودن تقویم جدید
        Hebrew
    }
}
```

#### 2. پیاده‌سازی کلاس تقویم جدید

```csharp
// مثال پیاده‌سازی تقویم عبری
using System;
using System.Globalization;
using CalendarEngine.Abstractions;

namespace CalendarEngine.Calendars
{
    public class HebrewCalendar : ICalendarConverter
    {
        private readonly System.Globalization.HebrewCalendar _calendar = new();

        public DateTime ConvertToGregorian(int year, int month, int day)
        {
            return _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public (int Year, int Month, int Day) ConvertFromGregorian(DateTime dateTime)
        {
            return (_calendar.GetYear(dateTime), _calendar.GetMonth(dateTime), _calendar.GetDayOfMonth(dateTime));
        }

        public string Format(DateTime dateTime, string format, CultureInfo? culture = null)
        {
            // Get Hebrew components
            var year = _calendar.GetYear(dateTime);
            var month = _calendar.GetMonth(dateTime);
            var day = _calendar.GetDayOfMonth(dateTime);
            
            // Use Hebrew culture if none specified
            culture ??= new CultureInfo("he-IL");
            
            // Replace tokens with Hebrew values
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
```

#### 3. ثبت تقویم جدید در کلاس اصلی

```csharp
// در فایل CalendarEngine.cs
private static Dictionary<CalendarType, ICalendarConverter> GetDefaultConverters()
{
    return new Dictionary<CalendarType, ICalendarConverter>
    {
        { CalendarType.Gregorian, new GregorianCalendar() },
        { CalendarType.Persian, new PersianCalendar() },
        { CalendarType.Hijri, new HijriCalendar() },
        // ثبت تقویم جدید
        { CalendarType.Hebrew, new HebrewCalendar() }
    };
}
```

#### 4. ثبت تقویم جدید در تزریق وابستگی

```csharp
// در فایل ServiceCollectionExtensions.cs
public static IServiceCollection AddCalendarEngine(this IServiceCollection services)
{
    // Register calendar converters
    services.AddSingleton<ICalendarConverter, GregorianCalendar>();
    services.AddSingleton<ICalendarConverter, PersianCalendar>();
    services.AddSingleton<ICalendarConverter, HijriCalendar>();
    // ثبت تقویم جدید
    services.AddSingleton<ICalendarConverter, HebrewCalendar>();
    
    // Register the calendar engine with all converters
    services.AddSingleton<CalendarEngine>(sp =>
    {
        var converters = new Dictionary<CalendarType, ICalendarConverter>
        {
            { CalendarType.Gregorian, sp.GetRequiredService<GregorianCalendar>() },
            { CalendarType.Persian, sp.GetRequiredService<PersianCalendar>() },
            { CalendarType.Hijri, sp.GetRequiredService<HijriCalendar>() },
            // ثبت تقویم جدید
            { CalendarType.Hebrew, sp.GetRequiredService<HebrewCalendar>() }
        };
        
        return new CalendarEngine(converters);
    });
    
    return services;
}
```

### بهبود قالب‌بندی موجود

اگر می‌خواهید قالب‌بندی یکی از تقویم‌های موجود را بهبود دهید، می‌توانید کلاس مربوطه را توسعه دهید:

```csharp
// مثال توسعه تقویم شمسی با قالب‌بندی بهبود یافته
public class EnhancedPersianCalendar : PersianCalendar
{
    // فهرست فارسی نام ماه‌ها
    private readonly string[] _monthNames = new[]
    {
        "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
        "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
    };
    
    // فهرست فارسی نام روزهای هفته
    private readonly string[] _dayNames = new[]
    {
        "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"
    };
    
    // بازنویسی متد Format برای پشتیبانی از قالب‌بندی بهتر
    public override string Format(DateTime dateTime, string format, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo("fa-IR");
        var components = ConvertFromGregorian(dateTime);
        
        // جایگزینی نام‌های ماه و روز
        if (format.Contains("MMMM"))
        {
            format = format.Replace("MMMM", _monthNames[components.Month - 1]);
        }
        else if (format.Contains("MMM"))
        {
            format = format.Replace("MMM", _monthNames[components.Month - 1].Substring(0, 3));
        }
        
        if (format.Contains("dddd"))
        {
            int dayOfWeek = (int)new System.Globalization.PersianCalendar().GetDayOfWeek(dateTime);
            format = format.Replace("dddd", _dayNames[dayOfWeek]);
        }
        else if (format.Contains("ddd"))
        {
            int dayOfWeek = (int)new System.Globalization.PersianCalendar().GetDayOfWeek(dateTime);
            format = format.Replace("ddd", _dayNames[dayOfWeek].Substring(0, 3));
        }
        
        // سایر قالب‌بندی‌ها با استفاده از متد پایه
        return base.Format(dateTime, format, culture);
    }
}
```

## عیب‌یابی مشکلات رایج

### خطای FormatException

**علت:** استفاده از الگوی قالب‌بندی نامعتبر

**راه حل:**
```csharp
try
{
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(DateTime.Now, "invalid-format");
}
catch (FormatException ex)
{
    Console.WriteLine($"خطای قالب‌بندی: {ex.Message}");
    // از یک قالب معتبر استفاده کنید
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(DateTime.Now, "yyyy/MM/dd");
}
```

### خطای تاریخ خارج از محدوده

**علت:** استفاده از تاریخی که خارج از محدوده یکی از تقویم‌هاست

**راه حل:**
```csharp
try
{
    var outOfRangeDate = new DateTime(10000, 1, 1);
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(outOfRangeDate, "yyyy/MM/dd");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"خطای تاریخ خارج از محدوده: {ex.Message}");
    // از یک تاریخ معتبر استفاده کنید
}
```

### خطای تقویم پشتیبانی نشده

**علت:** استفاده از نوع تقویمی که پیاده‌سازی نشده است

**راه حل:**
```csharp
try
{
    // فرض کنید CalendarType.Hebrew هنوز پیاده‌سازی نشده
    var result = CalendarEngine
        .From(CalendarType.Gregorian)
        .To((CalendarType)99) // تقویم نامعتبر
        .Format(DateTime.Now, "yyyy/MM/dd");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"خطای تقویم نامعتبر: {ex.Message}");
    // از یک تقویم معتبر استفاده کنید
}
```

</div>
