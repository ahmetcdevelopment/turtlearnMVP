using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtLearn.Shared.Utilities.Extensions
{
    public static class StringExtensions
    {
        //ChatGpt 3.5 tarafından 14.02.2024 01:53 tarihinde oluşturuldu.
        public static string GenerateRandomString(this string str, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
