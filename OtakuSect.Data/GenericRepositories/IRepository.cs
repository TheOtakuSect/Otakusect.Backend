using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.GenericRepositories
{
    public interface IRepository<T> 
    {
         Task<IEnumerable<T>> GetAllAsync();
         Task<T> GetByIdAsync(Guid id);
         Task<T> UpdateAsync(T item);
         Task<T> DeleteAsync(Guid id);
         Task<T> AddAsync(T item);
    }
}
