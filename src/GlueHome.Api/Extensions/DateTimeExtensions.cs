using System;

namespace GlueHome.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
        }
    }
}