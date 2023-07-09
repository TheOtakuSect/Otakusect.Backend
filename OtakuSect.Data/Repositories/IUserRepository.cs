using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System.Linq.Expressions;

namespace OtakuSect.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUser(string username, string password);
        public bool CheckUserName(string username);
        public bool CheckEmail(string email);
        Task<User> GetByIdAsync(Guid pId, params Expression<Func<User, object>>[] includes);

    }
}
