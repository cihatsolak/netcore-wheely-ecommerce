using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;

namespace Wheely.Core.Utilities
{
    public static class UrlExtension
    {
        public static string SlugUrl(this IUrlHelper helper, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return PrepareUrl(url);
        }

        public static string SlugUrl(params string[] keywords)
        {
            string url = string.Join("-", keywords);

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return PrepareUrl(url);
        }

        public static string SlugUrl(string formatableValue, params string[] keywords)
        {
            string url = string.Join("-", keywords);

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return string.Format(formatableValue, PrepareUrl(url));
        }

        private static string PrepareUrl(string url)
        {
            url = url.ToLower();
            url = url.Trim();
            url = url.Replace(" ", string.Empty);

            if (url.Length > 100)
            {
                url = url.Substring(0, 100);
            }

            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "o");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "u");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");

            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }

            Regex r = new("[^a-zA-Z0-9_-]");
            url = r.Replace(url, "-");

            while (url.IndexOf("--") > -1)
                url = url.Replace("--", "-");

            return url;
        }
    }
}
