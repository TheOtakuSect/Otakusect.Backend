using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.Context;
using System.Linq.Expressions;

namespace OtakuSect.Data.GenericRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        #region Methods
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<T> AddAsync(T item)
        {
            try
            {

                await _context.Set<T>().AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                return null;
            }
        }

        public T UpdateAsync(T item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
            return null;
        }
        #endregion
    }
}
