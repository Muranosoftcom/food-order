using System;
using System.Globalization;

namespace Core {
    public static class DateTimeExtension {
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek) {
            var start = (int) from.DayOfWeek;
            var target = (int) dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        public static string PrettifyDate(this DateTime dateTime) {
            return dateTime.ToString("d", CultureInfo.InvariantCulture);
        }
    }
}