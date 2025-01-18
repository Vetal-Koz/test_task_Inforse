using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Exceptions
{
    public class UnAuthenticatedException : Exception
    {
        public UnAuthenticatedException(string? message) : base(message)
        {
        }
    }
}
