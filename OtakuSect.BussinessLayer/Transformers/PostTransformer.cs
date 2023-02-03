using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Transformers
{
    public static class PostTransformer
    {
        public static List<PostResponse> GetPostResponseFromPost(List<Post> posts)
        {
            var postResponses = new List<PostResponse>();
            posts.ForEach((post) =>
            {
                var postResponse = new PostResponse()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    ViewCount = post.ViewCount,
                    IsSafeToWatch = post.IsSafeToWatch,
                    TotalRate = post.TotalRate,
                    PostedDateTime = post.PostedDateTime,
                    Tags = post.Tags,
                    Attachments = post.Attachments?.Select(x => x.Name).ToList(),
                    User = new PostUser() { Id = post.User.Id, UserName = post.User.UserName, Role = post.User.UserRole.Role }
                };
                postResponses.Add(postResponse);
            });
            return postResponses;
        }

        public static PostResponse GetPostResponseFromPost(Post post)
        {
            var postResponse = new PostResponse()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                ViewCount = post.ViewCount,
                IsSafeToWatch = post.IsSafeToWatch,
                TotalRate = post.TotalRate,
                PostedDateTime = post.PostedDateTime,
                Tags = post.Tags,
                Attachments = post.Attachments?.Select(x => x.Name).ToList(),
                User = post.User!=null ? new PostUser() { Id = post.User.Id, UserName = post.User.UserName, Role = post.User.UserRole.Role } : null
            };
            return postResponse;
        }
    }
}
