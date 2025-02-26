﻿using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class UserUpdateRequest
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public IFormFile File { get; set; }
    }
}
