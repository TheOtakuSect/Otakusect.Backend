using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.Data;
using OtakuSect.ViewModel;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("Admins")]
        [Authorize(Roles ="admin")]
       
        public IActionResult AdminsEndPoint()
        {
            var currentUser = GetCurrentUser();
            if (currentUser.Role != "admin")
            {
                return Unauthorized("Only sectmaster is allowed here");
            }
            return Ok($" Hi {currentUser.UserId} youre  an imortal being {currentUser.Role}");

        }

        private UserViewModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserViewModel
                {
                    UserId =Guid.Parse( userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,

                };
                return null;
            }
            return new UserViewModel();
        }
    }

}
