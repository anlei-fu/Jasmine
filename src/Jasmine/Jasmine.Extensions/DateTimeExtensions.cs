using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Extensions
{
  public static  class  DateTimeExtensions
    {
      
        public const long ONE_SECOND =1;
        public const long ONE_MINUTE =60;
        public const long ONE_HOUR =1000*60;
        public const long ONE_WEEK =1000*60*24*7;
        public const long ONE_MONTH = 1000 * 60  * 24 * 30;
        public const long ONE_YEAR =1000*60*24*365;
        public const long ONE_DAY =60*60*24;



        public static void Add( this DateTime time,int count,TimeUnit unit)
        {
            switch (unit)
            {
                case TimeUnit.Year:
                    time.AddYears(count);
                    break;
                case TimeUnit.Month:
                    time.AddMonths(count);
                    break;
                case TimeUnit.Week:
                    time.AddDays(count * 7);
                    break;
                case TimeUnit.Day:
                    time.AddDays(count);
                    break;
                case TimeUnit.Hour:
                    time.AddHours(count);
                    break;
                case TimeUnit.Minute:
                    time.AddMinutes(count);
                    break;
                case TimeUnit.Second:
                    time.AddSeconds(count);
                    break;
                case TimeUnit.MillionSecond:
                    time.AddMilliseconds(count);
                    break;
            }
        }
        public static DateTime Remove(this DateTime time,int count,TimeUnit unit)
        {
            switch (unit)
            {
                case TimeUnit.Year:
                    return new DateTime(time.Ticks - count * ONE_YEAR * 1000);
                case TimeUnit.Month:
                    return new DateTime(time.Ticks - count * ONE_MONTH * 1000);
                case TimeUnit.Week:
                    return new DateTime(time.Ticks - count * ONE_WEEK * 1000);
                case TimeUnit.Day:
                    return new DateTime(time.Ticks - count * ONE_DAY * 1000);
                case TimeUnit.Hour:
                    return new DateTime(time.Ticks - count * ONE_HOUR * 1000);
                case TimeUnit.Minute:
                    return new DateTime(time.Ticks - count * ONE_MINUTE * 1000);
                case TimeUnit.Second:
                    return new DateTime(time.Ticks - count * 1000);
                case TimeUnit.MillionSecond:
                    return new DateTime(time.Ticks - count );
                default:
                    return time;
            }
        }
    }
    public enum TimeUnit
    {
        Year,
        Month,
        Week,
        Day,
        Hour,
        Minute,
        Second,
        MillionSecond
    }
}
