using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class SlugHalper
    {
        public static string GenerateSlug(string phrase)
        {
            if(string.IsNullOrEmpty(phrase))
                return "";
            string str = phrase.ToLowerInvariant();
            str = str.Replace("ü", "u")
                .Replace("ğ", "g")
                .Replace("ç", "c")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s");

            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", "-").Trim('-');


            str = System.Text.RegularExpressions.Regex.Replace(str, @"-+", "-");

            return str;

        }
    }
}
