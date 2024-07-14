using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISingletonBase<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public Task<T?> FindAsync(params object[]? keyValues);
        public Task UpdateAsync(T entity, params object[]? keyValues);
        public Task DeleteAsync(params object[]? keyValues);
    }
}
