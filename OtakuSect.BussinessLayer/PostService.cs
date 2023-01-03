using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public class PostService : IPostService
    {
        public readonly IAttachmentService _attachmentService;
        public readonly IPostRepository _postRepository;
        public static ApiResponse<Post> apiResponse = new ApiResponse<Post>();

        public PostService(IAttachmentService attachmentService, IPostRepository postRepository)
        {
            _attachmentService = attachmentService;
            _postRepository = postRepository;
        }
        /// <summary>
        /// Posts Content by creating a new Post model and filling input from postViewModel
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        public ApiResponse<Post> PostContent(Guid uId, PostViewModel postViewModel)
        {
            var apiResponse = new ApiResponse<Post>();
            try
            {
                var newPost = new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = postViewModel.Title,
                    Description = postViewModel.Description,
                    Attachments = _attachmentService.UploadFile(postViewModel.Files),
                    IsSafeToWatch = postViewModel.IsSafeToWatch,
                    UserId = uId
                };
                _postRepository.AddAsync(newPost);
                apiResponse.StatusCode = 200;
                apiResponse.Success= true;
                apiResponse.Data = newPost;
                apiResponse.Message = "content posted successfully!!";
                return apiResponse;
            }
            catch
            {
                apiResponse.StatusCode = 500;
                apiResponse.Message = "content posted successfully!!";
                return apiResponse;
            }
        }
        /// <summary>
        /// Deletes Post through pId
        /// </summary>
        /// <param name="pId"></param>
        public ApiResponse<string> DeletePost(Guid pId)
        {
            _postRepository.DeleteAsync(pId);
            return new ApiResponse<string>();
        }
        /// <summary>
        /// Gets posts through pId
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<Post>> GetPostById(Guid pId)
        {
            try
            {
                var post =  await  _postRepository.GetByIdAsync(pId);
                apiResponse.StatusCode = 200;
                apiResponse.Success = true;
                apiResponse.Data = post;
                apiResponse.Message = "content fetched successfully!!";
                return apiResponse;
            }
            catch
            {
                apiResponse.StatusCode = 500;
                apiResponse.Message = "content couldnt be fetched successfully!!";
                return apiResponse;
            }
        }
    }
}
