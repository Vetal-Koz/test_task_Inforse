using InforseTestTask.Core.DTO.Request;
using InforseTestTask.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InforseTestTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutInfosController : ControllerBase
    {
        private readonly IAboutInfoService _aboutInfoService;

        public AboutInfosController(IAboutInfoService aboutInfoService)
        {
            _aboutInfoService = aboutInfoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AboutInfoRequest>> GetById([FromRoute] long id)
        {
            var result = await _aboutInfoService.FindById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] AboutInfoRequest request)
        {
            await _aboutInfoService.UpdateAsync(request, id);
            return NoContent();
        }
    }
}
