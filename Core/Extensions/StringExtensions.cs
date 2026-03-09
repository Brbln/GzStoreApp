using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string Normalize(this string value)
        {
            if(string.IsNullOrEmpty(value))
                return value;
            return value.Trim().ToLower();
        }
    }
}
