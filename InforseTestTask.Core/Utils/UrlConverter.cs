using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Utils
{
    public static class UrlConverter
    {
        public static string ShortedCode(string originalUrl)
        {
            var hash = originalUrl.GetHashCode();
            var shortCode = Convert.ToBase64String(BitConverter.GetBytes(hash)).TrimEnd('=');
            return  shortCode;
        }
    }
}
