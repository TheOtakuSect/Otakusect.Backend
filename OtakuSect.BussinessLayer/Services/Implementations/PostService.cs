using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Entities;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class PostService : IPostService
    {
        public readonly IAttachmentService _attachmentService;
        public readonly IPostRepository _postRepo;

        public PostService(IAttachmentService attachmentService, IPostRepository postRepo)
        {
            _attachmentService = attachmentService;
            _postRepo = postRepo;
        }

        public async Task<ApiResponse<List<PostResponse>>> GetAllPosts()
        {
            try
            {
                var result = await _postRepo.GetAllAsync(x => x.Attachments, x => x.User, x => x.User.UserRole);
                var response = PostTransformer.GetPostResponseFromPost(result.ToList());
                return ResponseCreater<List<PostResponse>>.CreateSuccessResponse(response, "Posts loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<PostResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<PostResponse>> GetPostById(Guid pId)
        {
            try
            {
                var post = await _postRepo.GetByIdAsync(pId);
                var response = PostTransformer.GetPostResponseFromPost(post);
                return ResponseCreater<PostResponse>.CreateSuccessResponse(response, "Post loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<PostResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public ApiResponse<PostResponse> PostContent(Guid userId, PostRequest PostRequest)
        {
            try
            {
                var post = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = PostRequest.Title,
                    Description = PostRequest.Description,
                    Attachments = PostRequest.Files?.Count > 0 ? _attachmentService.UploadFile(PostRequest.Files) : new List<Attachment>(),
                    IsSafeToWatch = PostRequest.IsSafeToWatch,
                    UserId = userId
                };
                _postRepo.AddAsync(post);
                var response = PostTransformer.GetPostResponseFromPost(post);
                return ResponseCreater<PostResponse>.CreateSuccessResponse(response, "Post created succcessfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<PostResponse>.CreateSuccessResponse(null, ex.ToString());
            }
        }

        public ApiResponse<string> DeletePost(Guid pId)
        {
            _postRepo.DeleteAsync(pId);
            return new ApiResponse<string>();
        }

        public async Task<ApiResponse<PostResponse>> EditPost(PostUpdateRequest postRequest)
        {
            try
            {
                var post = await _postRepo.GetByIdAsync(postRequest.Id);
                post.Title = postRequest.Title;
                post.Description = postRequest.Description;
                post.IsSafeToWatch = postRequest.IsSafeToWatch;
                post.Attachments = postRequest.Files?.Count > 0 ? _attachmentService.UploadFile(postRequest.Files) : new List<Attachment>();
                post.Tags = postRequest.Tags;
                _postRepo.UpdateAsync(post);

                var response = PostTransformer.GetPostResponseFromPost(post);
                return ResponseCreater<PostResponse>.CreateSuccessResponse(response, "Post updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<PostResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
