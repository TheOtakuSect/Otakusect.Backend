using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.GenericRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T item)
        {
            await _context.Set<T>.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<T> DeleteAsync(Guid id)
        {
            _context.Remove(id); 
            await _context.SaveChangesAsync();
            return null;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> UpdateAsync(T item)
        {
            _context.Set<T>().ExecuteUpdateAsync(item);
            _context.SaveChanges();
            return item;
        }
    }
}
