using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public static class Extensions
    {
        public static string ToBase64_UTF8(this string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        public static string FromBase64_UTF8(this string str) { Console.WriteLine(str); return Encoding.UTF8.GetString(Convert.FromBase64String(str)); }
    }
}
