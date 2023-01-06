using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.GenericRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        #region Generic add method
        public async Task<T> AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        #endregion

        #region Generic delete method
        public async Task<T> DeleteAsync(Guid id)
        {
            _context.Remove(id); 
            await _context.SaveChangesAsync();
            return null;

        }
        #endregion

        #region Generic get all
        public async Task<IEnumerable<T>> GetAllAsync(string predicate=null)
        {
            return await _context.Set<T>().Include(predicate??predicate).ToListAsync();
        }
        #endregion

        #region Generic get by id
        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        #endregion

        #region Generic Update method
        public async Task<T> UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            _context.SaveChanges();
            return t;
        }
        #endregion
    }
}
