using CoursePro.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursePro.API.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public ApiBaseController()
        {
            
        }

        public async Task<IActionResult> ResponseWrapperAsync<T>(Func<Task<T>> handle)
        {
            try
            {
                T data = await handle();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse<T>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = string.Empty,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }

        }
    }
}
