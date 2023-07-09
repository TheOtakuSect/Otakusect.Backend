using Microsoft.Extensions.Hosting;
using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Entities;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAttachmentService _attachmentService;
        public CommentService(ICommentRepository commentRepo, IAttachmentService attachmentService)
        {
            this._commentRepository = commentRepo;

            _attachmentService = attachmentService;

        }
        public async Task<ApiResponse<CommentResponse>> CreateComment(Guid userId, Guid PostId, CommentRequest commentRequest)
        {
            try
            {
                var comment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    Description = commentRequest.Description,
                    Attachments = commentRequest.Files?.Count > 0 ? _attachmentService.UploadFile(commentRequest.Files) : new List<Attachment>(),
                    UserId = userId,
                  
                };
                if (PostId != Guid.Empty)
                {
                    comment.PostId = PostId;
                }
                else
                {
                    throw new ArgumentException("Either postId or articleId must be provided.");
                }
               await _commentRepository.AddAsync(comment);
                var response = CommentTransformer.GetCommentResponseFromComment(comment);
                return ResponseCreater<CommentResponse>.CreateSuccessResponse(response, "comment created succcessfully");

            }
            catch(Exception ex)
            {

                return ResponseCreater<CommentResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public ApiResponse<string> DeleteComment(Guid commentId)
        {
            try
            {
                _commentRepository.DeleteAsync(commentId);
                return ResponseCreater<string>.CreateSuccessResponse("deleted", "Post deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<List<CommentResponse>>> GetCommentByArticleId(Guid Id)
        {
            try
            {
                var comments = await _commentRepository.GetByArticleId(Id, x => x.User, x => x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(comments.ToList());
                return ResponseCreater<List<CommentResponse>>.CreateSuccessResponse(response, "Posts loaded successfully.");

            }
            catch (Exception ex)
            {
                return ResponseCreater<List<CommentResponse>>.CreateErrorResponse(null, ex.ToString());

            }
        }

        public async Task<ApiResponse<CommentResponse>> GetCommentById(Guid Id)
        {
            try
            {
                var commment = await _commentRepository.GetByIdAsync (Id, x=>x.User, x=>x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(commment);
                return ResponseCreater<CommentResponse>.CreateSuccessResponse(response, "comment created succcessfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<CommentResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }
        
        public async Task<ApiResponse<List<CommentResponse>>> GetCommentByPostId(Guid Id)
        {
            try
            {
                var comments = await _commentRepository.GetByPostId(Id,x=>x.User,x=>x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(comments.ToList());
                return ResponseCreater<List<CommentResponse>>.CreateSuccessResponse(response, "Posts loaded successfully.");

            }
            catch (Exception ex)
            {
                return ResponseCreater<List<CommentResponse>>.CreateErrorResponse(null, ex.ToString());

            }
        }

        public async Task<ApiResponse<List<CommentResponse>>> GetCommentByUserId(Guid userId)
        {
            try
            {
                var comments = await _commentRepository.GetByUserId(userId, x => x.PostId, x => x.ArticleId, x => x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(comments.ToList());
                return ResponseCreater<List<CommentResponse>>.CreateSuccessResponse(response, "Posts loaded successfully.");

            }
            catch (Exception ex)
            {
                return ResponseCreater<List<CommentResponse>>.CreateErrorResponse(null, ex.ToString());

            }
        }


        public async Task<ApiResponse<CommentResponse>> EditComment( CommentUpdateRequest commentUpdate)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(commentUpdate.Id);
               comment.Description = commentUpdate.Description;
                comment.Attachments = commentUpdate.Files?.Count > 0 ? _attachmentService.UploadFile(commentUpdate.Files) : new List<Attachment>();

                _commentRepository.UpdateAsync(comment);

                var response = CommentTransformer.GetCommentResponseFromComment( comment);
                return ResponseCreater<CommentResponse>.CreateSuccessResponse(response, "Post updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<CommentResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<CommentResponse>> CreateReply(Guid userId, Guid parentcommentId, CommentRequest commentRequest)
        {

            try
            {
                if (parentcommentId == Guid.Empty)
                {
                    throw new ArgumentException("Either postId or articleId must be provided.");
                }
                else
                {

                    var reply = new Comment()
                    {
                        Id = Guid.NewGuid(),
                        ParentCommentId = parentcommentId,
                        Description = commentRequest.Description,
                        Attachments = commentRequest.Files?.Count > 0 ? _attachmentService.UploadFile(commentRequest.Files) : new List<Attachment>(),
                        UserId = userId,

                    };
                    await _commentRepository.AddAsync(reply);
                    var response = CommentTransformer.GetCommentResponseFromComment(reply);
                    return ResponseCreater<CommentResponse>.CreateSuccessResponse(response, "comment created succcessfully");
                }
               
              

            }
            catch (Exception ex)
            {

                return ResponseCreater<CommentResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<List<CommentResponse>>> GetReplys(Guid Id)
        {
            try
            {
                var reply = await _commentRepository.GetReplyIdAsync(Id, x => x.User, x => x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(reply);
                return ResponseCreater<List<CommentResponse>>.CreateSuccessResponse(response, "comment created succcessfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<CommentResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<List<CommentResponse>>> GetAllReplies(Guid Id)
        {
            try
            {
                var reply = await _commentRepository.GetReplyIdAsync(Id, x => x.User, x => x.Attachments);
                var response = CommentTransformer.GetCommentResponseFromComment(reply);
                return ResponseCreater<List<CommentResponse>>.CreateSuccessResponse(response, "comment created succcessfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<CommentResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
