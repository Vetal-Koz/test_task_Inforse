using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Exceptions
{
    public class UrlAlreadyExistException : Exception
    {
        public UrlAlreadyExistException(string? message) : base(message)
        {
        }
    }
}
