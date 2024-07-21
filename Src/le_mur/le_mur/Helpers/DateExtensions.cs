using System;

namespace le_mur.Helpers
{
    static class DateTimeExtensions
    {
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek day)
        {
            int diff = ((int)day - (int)date.DayOfWeek + 6) % 7;
            return date.AddDays(diff + 1);
        }
        public static DateTime MoveByWeek(this DateTime date)
        {
            return date.AddDays(7);
        }
        public static DateTime MoveByTwoWeeks(this DateTime date)
        {
            return date.AddDays(14);
        }
    }
}