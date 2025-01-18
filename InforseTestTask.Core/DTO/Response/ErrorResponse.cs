using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Response
{
    public class ErrorResponse : ApiResponse
    {
        public string Erorr { get; set; }

        public ErrorResponse(string message)
        {
            Erorr = message;
        }
    }
}
