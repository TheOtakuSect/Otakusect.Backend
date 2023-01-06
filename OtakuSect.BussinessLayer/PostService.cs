using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public class PostService : IPostService
    {
        public readonly IAttachmentService _attachmentService;
        public readonly IPostRepository _postRepository;
        public static ApiResponse<Post> apiResponse = new();

        #region Constructor
        public PostService(IAttachmentService attachmentService, IPostRepository postRepository)
        {
            _attachmentService = attachmentService;
            _postRepository = postRepository;
        }
        #endregion

        #region Post Content
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
        #endregion 

        #region Delete Content
        /// <summary>
        /// Deletes Post through pId
        /// </summary>
        /// <param name="pId"></param>
        public ApiResponse<string> DeletePost(Guid pId)
        {
            _postRepository.DeleteAsync(pId);
            return new ApiResponse<string>();
        }
        #endregion

        #region Get the post id
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
        #endregion

        #region Edit post region
        /// <summary>
        /// Edit the posts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse<Post>> EditPost(Guid id, PostViewModel postViewModel)
        {
            var apiResponse = new ApiResponse<Post>();
            try
            {
                var post = await _postRepository.GetByIdAsync(id);
                post.Title= postViewModel.Title;
                post.Description= postViewModel.Description;
                post.IsSafeToWatch= postViewModel.IsSafeToWatch;
                post.Tags= postViewModel.Tags;
                var fileList = postViewModel.Files;
                var list_attachment = new List<Attachment>();
                foreach (var file in fileList)
                {
                    var attachment =  new Attachment()
                    {
                        Id = Guid.NewGuid(),
                        Name = file.FileName
                    };
                    list_attachment.Add(attachment);
                }
                await _postRepository.UpdateAsync(post);
                apiResponse.Message = "post edited";
                apiResponse.StatusCode = 200;
                apiResponse.Success = true;
                apiResponse.Data = post;
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.StatusCode = 500;
                apiResponse.Message = ex.Message;
                return apiResponse;
            }
        }
        #endregion
    }
}
