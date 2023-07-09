using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface ICommentService
    {
        public Task<ApiResponse<CommentResponse>> CreateComment(Guid userId,Guid PostId,CommentRequest commentRequest);
            
        public Task<ApiResponse<List<CommentResponse>>> GetCommentByPostId(Guid Id);
        public Task<ApiResponse<CommentResponse>> GetCommentById(Guid Id);
        Task<ApiResponse<List<CommentResponse>>> GetCommentByArticleId(Guid Id);
        Task<ApiResponse<CommentResponse>> EditComment(CommentUpdateRequest commentUpdate);
        Task<ApiResponse<List<CommentResponse>>> GetCommentByUserId(Guid userId);
        public ApiResponse<string> DeleteComment(Guid commentId);
        public Task<ApiResponse<List<CommentResponse>>> GetReplys(Guid Id);
        public Task<ApiResponse<List<CommentResponse>>> GetAllReplies(Guid Id);

        public Task<ApiResponse<CommentResponse>> CreateReply(Guid userId,Guid parentcommentId, CommentRequest commentRequest);
    }

}
