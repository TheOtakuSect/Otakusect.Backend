using OtakuSect.Data;
using OtakuSect.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public interface IUserService
    {
        public Task<User> UpdateUser(Guid uId,UserUpdateViewModel userUpdateViewModel);
    }
}
