using InforseTestTask.Core.Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.DTO.Response
{
    public class UrlResponse : ApiResponse
    {
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserResponse CreatedBy { get; set; }

        public UrlResponse(ShortUrl shortUrl)
        {
            Id = shortUrl.Id;
            OriginalUrl = shortUrl.OriginalUrl;
            ShortenedUrl = shortUrl.ShortenedUrl;
            CreatedAt = shortUrl.CreatedDate;
            CreatedBy = new UserResponse(shortUrl.CreatedBy);
        }
    }

}
