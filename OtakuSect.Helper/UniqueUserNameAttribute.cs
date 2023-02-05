using OtakuSect.Data;
using System.ComponentModel.DataAnnotations;

namespace OtakuSect.Helper
{
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        private readonly AppDbContext _context;
        public UniqueUserNameAttribute(AppDbContext context)
        {
            _context = context;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            var userName = value.ToString();

            if (_context.Users.Any(u => u.UserName == userName))
            {
                return new ValidationResult("UserName already exists");
            }
            return ValidationResult.Success;
        }
    }
}
