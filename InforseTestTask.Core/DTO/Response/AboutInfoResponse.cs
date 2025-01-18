using InforseTestTask.Core.Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Response
{
    public class AboutInfoResponse : ApiResponse
    {
        public string Content { get; set; } = string.Empty;

        public AboutInfoResponse(AboutInfo info)
        {
            Id = info.Id;
            Content = info.Description;
        }
    }
}
