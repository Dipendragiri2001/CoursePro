using AuthService.Entities;
using AuthService.Interfaces;
using CoursePro.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoursePro.API.Controllers
{
    [Route("api/user")]
    public class UserManagementController : ApiBaseController
    {
        private readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRegisterModel model)
        {
            return await ResponseWrapperAsync(async () =>
            {
                return await _userService.CreateAsync(new ApplicationUser
                {
                    FullName = model.Name,
                    Email = model.Email
                }, model.Password);
            });


            return StatusCode((int)HttpStatusCode.OK, new ApiResponse<object>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true
            });
            
        }
    }
}
