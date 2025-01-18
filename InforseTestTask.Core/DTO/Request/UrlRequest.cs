using Microsoft.AspNetCore.Mvc;
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
        [Required(ErrorMessage = "Url can't be null")]
        [Url(ErrorMessage = "Url must be in appropriate apearance")]
        [Remote(action: "IsUrlAlreadyUsed", controller: "Urls", ErrorMessage = "Url already is used")]
        public string OriginalUrl { get; set; }
    }
}
