using InforseTestTask.Core.Domain.Entityes;
using InforseTestTask.Core.Domain.Repositories;
using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using InforseTestTask.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services.Impl
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public ShortUrlService(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public Task<UrlResponse> CreateAsync(UrlRequest req)
        {
            ShortUrl shortUrl = new ShortUrl();
            string shortedUrl = UrlConverter.ShortenUrl(req.OriginalUrl);

            shortUrl.OriginalUrl = req.OriginalUrl;
            shortUrl.ShortenedUrl = shortedUrl;
            shortUrl.CreatedDate = DateTime.Now;
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UrlResponse>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<UrlResponse> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UrlRequest req, long id)
        {
            throw new NotImplementedException();
        }
    }
}
