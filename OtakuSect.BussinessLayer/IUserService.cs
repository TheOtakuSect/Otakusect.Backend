using OtakuSect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public interface IUserService
    {
        public Task<User> UpdateUser(Guid id);

    }
}
