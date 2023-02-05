using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class UserRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
    }
}
