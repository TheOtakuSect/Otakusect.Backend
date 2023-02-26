using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Transformers
{
    public class CommentTransformer
    {
        public static List<CommentResponse> GetCommentResponseFromComment(List<Comment> comments)
        {
            var commentResponses = new List<CommentResponse>();
            comments.ForEach((comment) =>
            {
                var commentResponse = new CommentResponse()
                {
                    Id = comment.Id,
                    Description = comment.Description,
                    Upvote = comment.Upvote,
                    Downvote = comment.Downvote,
                    Attachments = comment.Attachments?.Select(x => x.Name).ToList(),
                    User = new CommentUser() { Id = comment.User.Id, UserName = comment.User.UserName}
                };
                commentResponses.Add(commentResponse);
            });
            return commentResponses;
        }

        public static CommentResponse GetCommentResponseFromComment(Comment comment)
        {
            var commentResponse = new CommentResponse()
            {
                Id = comment.Id,
                Description = comment.Description,
                Upvote = comment.Upvote,
                Downvote = comment.Downvote,
                Attachments = comment.Attachments?.Select(x => x.Name).ToList(),
                User = new CommentUser() { Id = comment.User.Id, UserName = comment.User.UserName }
            };
            return commentResponse;
        }
    }
}
