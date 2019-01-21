using System;
using System.Globalization;

namespace BusinessLogic.Utils {
    public static class DateUtils {
        public static string PrettifyDate(this DateTime dateTime) {
            return dateTime.ToString("d", CultureInfo.InvariantCulture);
        }
    }
}