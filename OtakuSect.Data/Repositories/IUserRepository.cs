using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUser(string username, string password);
        public bool CheckUserName(string username);
        public bool CheckEmail(string email);

    }
}
