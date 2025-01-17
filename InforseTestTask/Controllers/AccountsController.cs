using InforseTestTask.Core.Domain.Entityes.Indentity;
using InforseTestTask.Core.DTO.Auth;
using InforseTestTask.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InforseTestTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResposne>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                string erorrMessage = string.Join(" | ", ModelState.Values.SelectMany(v =>
                    v.Errors).Select(e => e.ErrorMessage));
                return Problem(erorrMessage);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                if (registerDTO.Roles != null && registerDTO.Roles.Any())
                {
                    var addRoleResult = await _userManager.AddToRolesAsync(user, registerDTO.Roles);
                    if (addRoleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        var authenticateResponse = _jwtService.CreateJwtToken(user, new List<string>(registerDTO.Roles));
                        return Ok(authenticateResponse);
                    }
                    else
                    {
                        string errorMessage = string.Join(" | ", addRoleResult.Errors.Select(e => e.Description));
                        return Problem(errorMessage, statusCode: 400);
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage, statusCode: 409);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResposne>> Login([FromBody] LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                var errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v =>
                v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var result = await _signInManager.PasswordSignInAsync(
                loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return NoContent();
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var authenticatinResponse = _jwtService.CreateJwtToken(user, roles);

                    return Ok(authenticatinResponse);
                }
            }
            else
            {
                return Problem("Invalid email or password");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpGet("alreadyExist")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
