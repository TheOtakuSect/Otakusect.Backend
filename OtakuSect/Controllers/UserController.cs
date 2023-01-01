using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OtakuSect.BussinessLayer;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.ViewModel;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("post")]
        public async Task<IActionResult> Update(Guid id)
        {
            var apiResponse = new ApiResponse<User>();
            try
            {
                var result = await _userService.UpdateUser(id);
                apiResponse.Success = true;
                apiResponse.StatusCode= 200;
                apiResponse.Data = result;
                apiResponse.Message = "User Updated Successfully";
                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.StatusCode= 500;
                apiResponse.Message= ex.Message;
                return BadRequest(ex.Message);  
            }
        }
    }

}
