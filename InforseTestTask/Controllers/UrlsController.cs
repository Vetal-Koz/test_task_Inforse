using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.DTO.Response;
using InforseTestTask.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InforseTestTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public UrlsController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UrlResponse>>> GetAll()
        {
            var response = await _shortUrlService.FindAll();
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UrlResponse>> Create([FromBody] UrlRequest urlRequest)
        {
            if(ModelState.IsValid)
            {
                var result = await _shortUrlService.CreateAsync(urlRequest);
                return Ok(result);
            }
            else
            {
                var errorMessage = string
                    .Join(" | ", ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UrlResponse>> GetById([FromRoute] long id)
        {
            var result = await _shortUrlService.FindById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteById([FromRoute] long id)
        {
            await _shortUrlService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("IsUrlAlreadyUsed")]
        public async Task<IActionResult> IsUrlAlreadyUsed(string url)
        {
            var result = await _shortUrlService.IsExistByOriginalUrl(url);
            return Ok(url);
        }

        [HttpGet("r/{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal([FromRoute] string shortCode)
        {
            var shortUrl = await _shortUrlService.FindByShortCodeAsync(shortCode);
            return Redirect(shortUrl.OriginalUrl);
        }
    }
}
