using OtakuSect.Data;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public interface IPostService
    {
        public ApiResponse<Post> PostContent(Guid uId, PostViewModel postViewModel);
        public ApiResponse<string> DeletePost(Guid pId);
        public Task<ApiResponse<Post>> GetPostById(Guid pId);
        public Task<ApiResponse<Post>> EditPost(Guid pId, PostViewModel postViewModel);
    }
}

