using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderScore(this DateTime dateTime)
        {
            return
                $"{dateTime.Millisecond}-{dateTime.Second}-{dateTime.Minute}-{dateTime.Hour}-{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
        }
    }
}
