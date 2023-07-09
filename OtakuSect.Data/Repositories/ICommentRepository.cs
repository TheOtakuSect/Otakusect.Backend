using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetByIdAsync(Guid pId, params Expression<Func<Comment, object>>[] includes);
        Task<IEnumerable<Comment>> GetByPostId(Guid Id, params Expression<Func<Comment, object>>[] includes);
        Task<IEnumerable<Comment>> GetByArticleId(Guid Id, params Expression<Func<Comment, object>>[] includes);
        Task<IEnumerable<Comment>> GetByUserId(Guid Id, params Expression<Func<Comment, object>>[] includes);

        Task<List<Comment>> GetReplyIdAsync(Guid Id, params Expression<Func<Comment, object>>[] includes);


        
    }

}
