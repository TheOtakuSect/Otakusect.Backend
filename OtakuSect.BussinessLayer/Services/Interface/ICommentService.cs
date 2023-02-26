using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface ICommentService
    {
        public Task<ApiResponse<List<CommentResponse>>> GetAllComments();
        public Task<ApiResponse<CommentResponse>> GetCommentById(Guid postId);
        public ApiResponse<CommentResponse> PostComment(Guid userId, CommentRequest commentRequest);
        public Task<ApiResponse<CommentResponse>> EditComment(PostUpdateRequest postRequest);
        public ApiResponse<string> DeleteComment(Guid postId);
    }
}
