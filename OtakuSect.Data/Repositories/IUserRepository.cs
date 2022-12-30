using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserNameandPassword(string username,string password);
    }
}
