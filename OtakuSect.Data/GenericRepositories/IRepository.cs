﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.GenericRepositories
{
    public interface IRepository<T> 
    {
         Task<IEnumerable<T>> GetAllAsync(string predicate);
         Task<T> GetByIdAsync(Guid Id);
         Task<T> UpdateAsync( T t);
         Task<T> DeleteAsync(Guid id);
         Task<T> AddAsync(T item);
    }
}
