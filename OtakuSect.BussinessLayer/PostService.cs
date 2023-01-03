using OtakuSect.Data;
using OtakuSect.Data.Repositories;

namespace OtakuSect.BussinessLayer
{
    public class PostService : IPostService
    {
        public readonly IAttachmentService _attachmentService;
        public readonly IPostRepository _postRepository;
        public static ApiResponse<Data.PostViewModel> apiResponse = new ApiResponse<Data.PostViewModel>();

        public PostService(IAttachmentService attachmentService, IPostRepository postRepository)
        {
            _attachmentService = attachmentService;
            _postRepository = postRepository;
        }

        public void DeletePost(Guid Id)
        {
            _postRepository.DeleteAsync(Id);
        }

        public Task<Data.PostViewModel> EditPost(ViewModel.PostViewModel postViewModel)
        {
            throw new NotImplementedException();
        }


        public Task<Data.PostViewModel> GetPostById(Guid id, ViewModel.PostViewModel postViewModel)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<Data.PostViewModel> PostContent(Guid uId, ViewModel.PostViewModel postViewModel)
        {

            try
            {
                var newPost = new Data.PostViewModel()
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
    }
}
