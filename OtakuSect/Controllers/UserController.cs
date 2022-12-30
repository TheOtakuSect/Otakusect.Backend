using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.ViewModel;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
    }

}
