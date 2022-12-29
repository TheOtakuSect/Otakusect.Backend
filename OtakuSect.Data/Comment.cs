using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ParentCommentId { get; set; }
        public string Description { get; set; } 
        public int Upvote { get; set; }
        public int Downvote { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set;}

        public DateTime CommentDateTime { get; set; } = DateTime.Now;

        public Post Post { get; set; }
        public Guid PostId { get; set; }

    }
}
