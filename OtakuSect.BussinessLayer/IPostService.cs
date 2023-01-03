using OtakuSect.Data;
namespace OtakuSect.BussinessLayer
{
    public interface IPostService
    {
        public ApiResponse<Data.PostViewModel> PostContent(Guid uId, ViewModel.PostViewModel postViewModel);
        public void DeletePost(Guid Id);
        public Task<Data.PostViewModel> EditPost(ViewModel.PostViewModel postViewModel);
        public Task<Data.PostViewModel> GetPostById(Guid id, ViewModel.PostViewModel postViewModel);
    }
}
