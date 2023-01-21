using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public class UserRepository:Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) {
        }

        public bool CheckUserName(string username)
        {
            var user = _context.Users.Any(x => x.UserName == username);
            if (user== true)
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


        public async Task<User> GetUserNameandPassword(string username,string password)
        {
            var current_user = await _context.Users.Include(x=>x.UserRole).FirstOrDefaultAsync(
                o => o.UserName.ToLower() == username.ToLower() && o.Password == password);
            return current_user;
        }
    }
}
