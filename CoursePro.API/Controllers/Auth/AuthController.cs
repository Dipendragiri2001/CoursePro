using AuthService;
using AuthService.Interfaces;
using AuthService.Models;
using AutoMapper;
using CoursePro.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursePro.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public AuthController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                AuthResultModel result = _mapper.Map<AuthResultModel>(await _authenticationService.LoginAsync(_mapper.Map<UserLoginModel>(model)));
                if (result.Succeeded)
                {
                    IssueCookie(HttpContext, result);
                    return Ok(new { IsAuthenticated = result.Succeeded, Roles = result.Roles, ExpiresAt = result.ExpiresAt });
                }
                ModelState.AddModelError("Unauthorized", result.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Unauthorized", "Please check the username and password and retry to login.");
            }
            return Unauthorized(ModelState);


        }

        [HttpGet]
        [Route("test")]
        [Authorize]
        public async Task<IActionResult> AuthTest()
        {
            return Ok("Success");
        }


        private void IssueCookie(HttpContext context, AuthResultModel result)
        {
            context.Response.Cookies.Append(AuthConstants.AccessToken, result.Token, new CookieOptions
            {
                Domain = "localhost",
                Path = "/",
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Expires = result.ExpiresAt
            });
        }
    }
}
