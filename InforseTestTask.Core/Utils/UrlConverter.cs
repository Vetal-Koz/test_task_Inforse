using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Utils
{
    public static class UrlConverter
    {
        private static string baseUrl = "https://shorted/";

        public static string ShortenUrl(string originalUrl)
        {
            var hash = originalUrl.GetHashCode();
            var shortCode = Convert.ToBase64String(BitConverter.GetBytes(hash)).TrimEnd('=');
            return baseUrl + shortCode;
        }
    }
}
