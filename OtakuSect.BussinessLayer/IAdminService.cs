using OtakuSect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public interface IAdminService
    {
        public Task<ApiResponse<User>> ChangeRole(Guid uId);
        public IAsyncEnumerable<User> GetAllUser();


    }
}
