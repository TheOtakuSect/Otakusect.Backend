using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public UserRole UserRole { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
