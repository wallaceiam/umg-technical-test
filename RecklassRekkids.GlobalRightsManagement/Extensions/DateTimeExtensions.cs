using System;
using System.Globalization;

namespace RecklassRekkids.GlobalRightsManagement.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string[] Formats = new[] {
            @"d\s\t MMM yyyy",
            @"d\t\h MMM yyyy",
            @"d\r\d MMM yyyy",
            @"d\n\d MMM yyyy",
            @"d\s\t MMMM yyyy",
            @"d\t\h MMMM yyyy",
            @"d\r\d MMMM yyyy",
            @"d\n\d MMMM yyyy"
        };

        public static bool TryParseDate(this string dateTime, out DateTime date) =>
            DateTime.TryParseExact(dateTime, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        public static string FormatDate(this DateTime dateTime) =>
           dateTime.Month == 6 || dateTime.Month == 7 ? 
            $"{dateTime.Day.AsOrdinal()} {dateTime:MMMM yyyy}" : $"{dateTime.Day.AsOrdinal()} {dateTime:MMM yyyy}";

        public static string FormatDate(this DateTime? dateTime) =>
            dateTime.HasValue ? FormatDate(dateTime.Value) : string.Empty;

        public static string AsOrdinal(this int number)
        {
            if (number <= 0 || number > 31)
                throw new ArgumentOutOfRangeException(nameof(number));

            var work = number.ToString("n0");

            var modOf100 = number % 100;

            if (modOf100 == 11 || modOf100 == 12 || modOf100 == 13)
                return work + "th";

            switch (number % 10)
            {
                case 1:
                    work += "st"; break;
                case 2:
                    work += "nd"; break;
                case 3:
                    work += "rd"; break;
                default:
                    work += "th"; break;
            }

            return work;
        }
    }
}
