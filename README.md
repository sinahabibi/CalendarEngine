
# CalendarEngine

> **Language Selection | انتخاب زبان:**
>
> - [English](#english)
> - [فارسی (Persian)](#persian)

<a name="english"></a>
# CalendarEngine - Multi-Calendar Conversion Library

A flexible and extensible .NET library for converting dates between different calendar systems including Gregorian, Persian (Jalali), and Hijri (Islamic) calendars with a fluent API.

## Features

- Fluent API for easy date conversion and formatting
- Support for Gregorian, Persian (Jalali), and Hijri (Islamic) calendars
- Extensible design to add new calendar systems
- Advanced date formatting with culture-specific options
- Dependency Injection support

## Installation

Install the package from NuGet:

```bash
dotnet add package CalendarEngine
```

Or using the Package Manager Console:

```powershell
Install-Package CalendarEngine
```

## Usage

### Basic Conversion

```csharp
// Convert current date from Gregorian to Persian
var persianDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(DateTime.Now, "yyyy/MM/dd");

// Convert date from Gregorian to Hijri
var hijriDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(new DateTime(2023, 12, 25), "yyyy/MM/dd");
```

### Format with Different Patterns

```csharp
var now = DateTime.Now;
string[] persianFormats = { "yyyy/MM/dd", "yy/MM/dd", "yyyy-MM-dd", "d MMMM yyyy", "dddd, d MMMM yyyy" };

foreach (var format in persianFormats)
{
    var formatted = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(now, format);
    Console.WriteLine($"{format} → {formatted}");
}
```

### Date Conversion Between Calendar Systems

```csharp
// Persian to Gregorian conversion
PersianCalendar pc = new PersianCalendar();
var persianDateTime = new DateTime(1402, 1, 1, pc);
var gregorianEquivalent = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDateTime);

// Round trip conversion
var backToPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(gregorianEquivalent, "yyyy/MM/dd");
```

### Cultural Dates

```csharp
// Working with special cultural dates
PersianCalendar pc = new PersianCalendar();
var nowruz = new DateTime(1402, 1, 1, pc); // Persian New Year

HijriCalendar hc = new HijriCalendar();
var islamicNewYear = new DateTime(1445, 1, 1, hc); // Islamic New Year

// Convert holidays between calendars
var christmas = new DateTime(2023, 12, 25);
var christmasInPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(christmas, "yyyy/MM/dd");
```

### Using Dependency Injection

```csharp
// Register the services in your startup
services.AddCalendarEngine();

// Then use it in your services
public class MyService
{
    private readonly ICalendarEngine _calendarEngine;

    public MyService(ICalendarEngine calendarEngine)
    {
        _calendarEngine = calendarEngine;
    }

    public string GetCurrentPersianDate()
    {
        return _calendarEngine
            .CreateConverter(CalendarType.Gregorian)
            .To(CalendarType.Persian)
            .Format(DateTime.Now, "yyyy/MM/dd");
    }
}
```

## Extending the Library

1. Add a new value to the `CalendarType` enum
2. Implement `ICalendarConverter` for your new calendar
3. Register your calendar in the default converters

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<a name="persian"></a>
<div dir="rtl">

# کتابخانه تبدیل تقویم (CalendarEngine)

یک کتابخانه‌ی .NET انعطاف‌پذیر و قابل گسترش برای تبدیل تاریخ بین سیستم‌های تقویمی مختلف، شامل تقویم میلادی (گرگوری)، شمسی (جلالی) و هجری قمری با API روان.

## ویژگی‌ها

- API روان برای تبدیل و قالب‌بندی آسان تاریخ
- پشتیبانی از تقویم‌های میلادی، شمسی و هجری قمری
- طراحی قابل گسترش برای افزودن سیستم‌های تقویمی جدید
- قالب‌بندی پیشرفته تاریخ با پشتیبانی از فرهنگ‌ها
- پشتیبانی از تزریق وابستگی (Dependency Injection)

## نصب

نصب از طریق NuGet:

```bash
dotnet add package CalendarEngine
```

یا با استفاده از Package Manager Console:

```powershell
Install-Package CalendarEngine
```

## نحوه استفاده

### تبدیل پایه

```csharp
// تبدیل تاریخ فعلی از میلادی به شمسی
var persianDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(DateTime.Now, "yyyy/MM/dd");

// تبدیل تاریخ از میلادی به هجری قمری
var hijriDate = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Hijri)
    .Format(new DateTime(2023, 12, 25), "yyyy/MM/dd");
```

### قالب‌بندی با الگوهای مختلف

```csharp
var now = DateTime.Now;
string[] persianFormats = { "yyyy/MM/dd", "yy/MM/dd", "yyyy-MM-dd", "d MMMM yyyy", "dddd, d MMMM yyyy" };

foreach (var format in persianFormats)
{
    var formatted = CalendarEngine
        .From(CalendarType.Gregorian)
        .To(CalendarType.Persian)
        .Format(now, format);
    Console.WriteLine($"{format} → {formatted}");
}
```

### تبدیل تاریخ بین تقویم‌ها

```csharp
// تبدیل از شمسی به میلادی
PersianCalendar pc = new PersianCalendar();
var persianDateTime = new DateTime(1402, 1, 1, pc);
var gregorianEquivalent = CalendarEngine
    .From(CalendarType.Persian)
    .To(CalendarType.Gregorian)
    .Convert(persianDateTime);

// تبدیل برگشتی
var backToPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(gregorianEquivalent, "yyyy/MM/dd");
```

### تاریخ‌های فرهنگی

```csharp
// کار با تاریخ‌های فرهنگی خاص
PersianCalendar pc = new PersianCalendar();
var nowruz = new DateTime(1402, 1, 1, pc); // نوروز

HijriCalendar hc = new HijriCalendar();
var islamicNewYear = new DateTime(1445, 1, 1, hc); // سال نو هجری

// تبدیل مناسبت‌ها بین تقویم‌ها
var christmas = new DateTime(2023, 12, 25);
var christmasInPersian = CalendarEngine
    .From(CalendarType.Gregorian)
    .To(CalendarType.Persian)
    .Format(christmas, "yyyy/MM/dd");
```

### استفاده از تزریق وابستگی

```csharp
// ثبت سرویس‌ها
services.AddCalendarEngine();

// استفاده در سرویس
public class MyService
{
    private readonly ICalendarEngine _calendarEngine;

    public MyService(ICalendarEngine calendarEngine)
    {
        _calendarEngine = calendarEngine;
    }

    public string GetCurrentPersianDate()
    {
        return _calendarEngine
            .CreateConverter(CalendarType.Gregorian)
            .To(CalendarType.Persian)
            .Format(DateTime.Now, "yyyy/MM/dd");
    }
}
```

## گسترش کتابخانه

1. افزودن مقدار جدید به enum با نام `CalendarType`
2. پیاده‌سازی رابط `ICalendarConverter` برای تقویم جدید
3. ثبت مبدل در مبدل‌های پیش‌فرض

## مجوز

این پروژه تحت مجوز MIT منتشر شده است - برای جزئیات فایل [LICENSE](LICENSE) را ببینید.

</div>
