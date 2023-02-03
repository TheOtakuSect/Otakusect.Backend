using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Transformers
{
    public static class UserTransformer
    {
        public static List<UserResponse> GetUserResponseFromUser(List<User> users)
        {
            List<UserResponse> userResponses = new();
            users.ForEach(user =>
            {
                userResponses.Add(new UserResponse
                {
                    UserName = user.UserName,
                    Email = user.EmailAddress,
                    ProfilePic = user.ProfilePic,
                    FullName = user.FullName,
                    Role = user.UserRole.Role,
                });
            });
            return userResponses;
        }

        public static UserResponse GetUserResponseFromUser(User user)
        {
            UserResponse userResponse = new()
            {
                UserName = user.UserName,
                Email = user.EmailAddress,
                ProfilePic = user.ProfilePic,
                FullName = user.FullName,
                Role = user.UserRole.Role,
            };
            return userResponse;
        }

    }
}
