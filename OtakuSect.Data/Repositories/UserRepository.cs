using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.Context;
using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System.Linq.Expressions;

namespace OtakuSect.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public bool CheckUserName(string username)
        {
            var user = _context.Users.Any(x => x.UserName == username);
            if (user == true)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail(string email)
        {
            var user = _context.Users.Any(x => x.EmailAddress == email);
            if (user == true)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var current_user = await _context.Users.Include(x => x.UserRole).FirstOrDefaultAsync(
                o => o.UserName.ToLower() == username.ToLower() && o.Password == password);
            return current_user;
        }

        public async Task<User> GetByIdAsync(Guid Id, params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> query = _context.Set<User>();

            // Apply any included navigation properties to the query
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Execute the query to retrieve the entity by its Id
            User entity = await query.FirstOrDefaultAsync(e => e.Id == Id);

            return entity;
        }
    }
}
