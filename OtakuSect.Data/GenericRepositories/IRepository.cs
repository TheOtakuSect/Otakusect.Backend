using System.Linq.Expressions;

namespace OtakuSect.Data.GenericRepositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(Guid Id, params Expression<Func<T, object>>[] includes);
        T UpdateAsync(T t);
        Task<T> DeleteAsync(Guid id);
        Task<T> AddAsync(T item);
    }
}
