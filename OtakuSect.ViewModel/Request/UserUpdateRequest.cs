using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public IFormFile File { get; set; }
    }
}
