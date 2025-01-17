using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Request
{
    public class UrlRequest : ApiRequest
    {
        [Required]
        [Url]
        public string OriginalUrl { get; set; }
    }
}
