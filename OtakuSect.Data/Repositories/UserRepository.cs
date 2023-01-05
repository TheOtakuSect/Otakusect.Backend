using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public class UserRepository:Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) {
        }
            
        public async Task<User> GetUserNameandPassword(string username,string password)
        {
            var current_user = await _context.Users.Include(x=>x.UserRole).FirstOrDefaultAsync(
                o => o.UserName.ToLower() == username.ToLower() && o.Password == password);
            return current_user;
        }
    }
}
