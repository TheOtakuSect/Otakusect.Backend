﻿using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IUserService
    {
        public Task<ApiResponse<UserResponse>> UpdateUser(Guid userId,UserUpdateRequest userUpdateRequest);
        public ApiResponse<bool> CheckUser(string userName);
    }
}
