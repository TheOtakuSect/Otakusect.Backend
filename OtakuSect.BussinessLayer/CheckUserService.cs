using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OtakuSect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public class CheckUserService 
    {
        private readonly IUserRepository userRepository;

        public CheckUserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool CheckUser(string userName)
        {
            return userRepository.CheckUserName(userName);
        }
    }
}
