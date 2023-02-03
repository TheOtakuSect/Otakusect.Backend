using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IPostService
    {
        public Task<ApiResponse<List<PostResponse>>> GetAllPosts();
        public Task<ApiResponse<PostResponse>> GetPostById(Guid postId);
        public ApiResponse<PostResponse> PostContent(Guid userId, PostRequest postRequest);
        public Task<ApiResponse<PostResponse>> EditPost(PostUpdateRequest postRequest);
        public ApiResponse<string> DeletePost(Guid postId);
    }
}

