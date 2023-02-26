using Microsoft.Extensions.Hosting;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Entities;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IAttachmentService _attachmentService;
        private readonly ICommentRepository _commentRepository;
        public CommentService(IAttachmentService attachmentService, ICommentRepository commentRepository)
        {
            _attachmentService = attachmentService;
            _commentRepository = commentRepository;
        }
        public ApiResponse<string> DeleteComment(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CommentResponse>> EditComment(PostUpdateRequest postRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<CommentResponse>>> GetAllComments()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CommentResponse>> GetCommentById(Guid postId)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<CommentResponse> PostComment(Guid userId, CommentRequest commentRequest)
        {
            try
            {
                var cmnt = new Comment();
                cmnt.Id = new Guid();
                cmnt.Description = commentRequest.Description;
                cmnt.Attachments = _attachmentService.UploadFile(commentRequest.Files);
                _commentRepository.AddAsync(cmnt);
                var response = PostTransformer.GetPostResponseFromPost(post);

            }
            catch (Exception ex)
            {

            }


        }
    }
}
