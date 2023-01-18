using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel
{
    public class UserUpdateViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public IFormFile File { get; set; }
    }
}
