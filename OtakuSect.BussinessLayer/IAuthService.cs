using OtakuSect.Data;
using OtakuSect.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity);
        public Task<User> Register(UserViewModel user);
        public Task<string?> Login(string userName, string password);
    }
}
