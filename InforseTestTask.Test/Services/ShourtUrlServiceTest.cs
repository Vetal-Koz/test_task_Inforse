using InforseTestTask.Core.Domain.Repositories;
using System.Security.Claims;
using Moq;
using Microsoft.AspNetCore.Identity;
using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.Services.Impl;
using Microsoft.AspNetCore.Http;
using InforseTestTask.Core.Domain.Entityes;
using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.Exceptions;

namespace InforseTestTask.Test.Services
{
    public class ShourtUrlServiceTest
    {
        private readonly Mock<IShortUrlRepository> _shortUrlRepositoryMock = new();
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly ShortUrlService _shortUrlService;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public ShourtUrlServiceTest()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(); 
            var context = new DefaultHttpContext(); 
            _httpContextAccessorMock.Setup(_ => _.HttpContext).Returns(context); 

            _shortUrlService = new ShortUrlService(
                _shortUrlRepositoryMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenUrlAlreadyExists()
        {
            string originalUrl = "http://example.com";
            _shortUrlRepositoryMock.Setup(repo => repo.IsExistByOriginalUrl(originalUrl)).ReturnsAsync(true);

            await Assert.ThrowsAsync<UrlAlreadyExistException>(() => _shortUrlService.CreateAsync(new UrlRequest { OriginalUrl = originalUrl }));
        }

        [Fact]
        public async Task DeleteAsync_DeletesUrl_WhenUserIsAdmin()
        {
            var url = new ShortUrl { Id = 1, CreatedBy = new ApplicationUser { Email = "user@example.com" } };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, "admin@example.com")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);
            _shortUrlRepositoryMock.Setup(repo => repo.FindByIdAsync(1)).ReturnsAsync(url);

            await _shortUrlService.DeleteAsync(1);

            _shortUrlRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowsUnauthorizedAccessException_WhenUserIsNotOwnerOrAdmin()
        {
            var url = new ShortUrl { Id = 1, CreatedBy = new ApplicationUser { Email = "owner@example.com" } };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, "otheruser@example.com")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);
            _shortUrlRepositoryMock.Setup(repo => repo.FindByIdAsync(1)).ReturnsAsync(url);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _shortUrlService.DeleteAsync(1));
        }

        [Fact]
        public async Task FindAll_ReturnsListOfUrlResponses()
        {
            var user = new ApplicationUser { Email = "user@example.com" };

            var urls = new List<ShortUrl>
            {
                new ShortUrl
                {
                    Id = 1,
                    OriginalUrl = "http://example.com",
                    ShortenedUrl = "http://short.ly/1",
                    CreatedBy = user 
                },
                new ShortUrl
                {
                    Id = 2,
                    OriginalUrl = "http://test.com",
                    ShortenedUrl = "http://short.ly/2",
                    CreatedBy = user
                }
            };

            _shortUrlRepositoryMock.Setup(repo => repo.FindAllAsync()).ReturnsAsync(urls);

            var result = await _shortUrlService.FindAll();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.ShortenedUrl == "http://short.ly/1");
            Assert.Contains(result, r => r.ShortenedUrl == "http://short.ly/2");
        }


        [Fact]
        public async Task FindById_ReturnsUrlResponse_WhenUrlExists()
        {
            var user = new ApplicationUser { Email = "user@example.com" };
            var url = new ShortUrl
            {
                Id = 1,
                OriginalUrl = "http://example.com",
                ShortenedUrl = "http://short.ly/1",
                CreatedBy = user 
            };


            _shortUrlRepositoryMock.Setup(repo => repo.FindByIdAsync(1)).ReturnsAsync(url);
            var result = await _shortUrlService.FindById(1);

            Assert.NotNull(result);
            Assert.Equal("http://short.ly/1", result.ShortenedUrl);
        }

        [Fact]
        public async Task FindById_ThrowsEntityNotFoundException_WhenUrlDoesNotExist()
        {
            _shortUrlRepositoryMock.Setup(repo => repo.FindByIdAsync(1)).ReturnsAsync((ShortUrl)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _shortUrlService.FindById(1));
        }

    }
}
