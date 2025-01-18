using InforseTestTask.Core.Domain.Entityes;
using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.Domain.Repositories;
using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using InforseTestTask.Core.Exceptions;
using InforseTestTask.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InforseTestTask.Core.Services.Impl
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userMananger;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _shortUrlRepository = shortUrlRepository;
            _httpContextAccessor = httpContextAccessor;
            _userMananger = userManager;
        }

        public async Task<UrlResponse> CreateAsync(UrlRequest req)
        {
            if (await _shortUrlRepository.IsExistByOriginalUrl(req.OriginalUrl))
            {
                throw new UrlAlreadyExistException("Such url already exists");
            }

            var shortUrl = await CreateShortUrl(req);
            var createdShortUrl = await _shortUrlRepository.CreateAsync(shortUrl);
            return new UrlResponse(createdShortUrl);
        }

        public async Task DeleteAsync(long id)
        {
            var userClaims = _httpContextAccessor.HttpContext.User;
            var url = await _shortUrlRepository.FindByIdAsync(id);
            if (url == null)
            {
                throw new EntityNotFoundException("Entity not found");
            }

            if (userClaims.IsInRole("Admin") || IsOwnerOfUrl(userClaims, url))
            {
                await _shortUrlRepository.DeleteAsync(id);
            }
            else
            {
                throw new UnauthorizedAccessException("User does not have permission to delete this URL");
            }
        }

        public async Task<List<UrlResponse>> FindAll()
        {
            var urls = await _shortUrlRepository.FindAllAsync();
            List<UrlResponse> responses = urls.Select(u => new UrlResponse(u)).ToList();
            return responses;
        }

        public async Task<UrlResponse> FindById(long id)
        {
            var url = await _shortUrlRepository.FindByIdAsync(id);
            if(url == null)
            {
                throw new EntityNotFoundException("Entity not found");
            }
            return new UrlResponse(url);
        }

        public async Task<UrlResponse> FindByShortCodeAsync(string shortCode)
        {
            var shortUrl = await _shortUrlRepository.FindByShortCodeAsync(shortCode);
            if(shortUrl == null)
            {
                throw new EntityNotFoundException("Entity not found");
            }
            return new UrlResponse(shortUrl);
        }

        public async Task<bool> IsExistByOriginalUrl(string originalUrl)
        {
            return await _shortUrlRepository.IsExistByOriginalUrl(originalUrl);
        }

        public Task UpdateAsync(UrlRequest req, long id)
        {
            throw new NotImplementedException();
        }

        private async Task<ShortUrl> CreateShortUrl(UrlRequest req)
        {
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/api/urls/r/";
            var shortCode = UrlConverter.ShortedCode(req.OriginalUrl);
            var shortUrl = new ShortUrl
            {
                OriginalUrl = req.OriginalUrl,
                ShortenedUrl = baseUrl + shortCode,
                CreatedDate = DateTime.UtcNow
            };
            var userClaims = _httpContextAccessor.HttpContext.User;
            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userMananger.FindByEmailAsync(email);
            shortUrl.CreatedBy = user;

            return shortUrl;
        }

        private bool IsOwnerOfUrl(ClaimsPrincipal userClaims, ShortUrl url)
        {
            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value?.ToLower();
            return email != null && email == url.CreatedBy.Email.ToLower();
        }
    }
}
