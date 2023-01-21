using OtakuSect.Data.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        public Task<User> GetUserNameandPassword(string username,string password);
        public bool CheckUserName(string username);
        public bool CheckEmail(string email);

    }
}
