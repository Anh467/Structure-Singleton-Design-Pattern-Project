using BusinessObject;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SingletonBase<U, T>: ISingletonBase<T> where U : class, new() where T : class 
    {
        private static U _instace;
        private static readonly object _lock = new object();    
        public static ApplicationDBContext _context = new ApplicationDBContext();

        public static U Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instace == null)
                    {
                        _instace = new U();
                    }
                    return _instace;
                }
            }
        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> list = await _context.Set<T>().ToListAsync();
            return list;
        }

        public async Task<T?> FindAsync(params object[]? keyValues)
        {
            T? temp = await _context.Set<T>().FindAsync(keyValues);
            return temp;
        }

        public async Task UpdateAsync(T entity, params object[]? keyValues)
        {
            var temp = await _context.Set<T>().FindAsync(keyValues);
            if (temp != null)
            {
                _context.Entry(temp).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Set<T>().Add(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(params object[]? keyValues)
        {
            var temp = await _context.Set<T>().FindAsync(keyValues);
            if (temp != null)
                _context.Set<T>().Remove(temp);
            await _context.SaveChangesAsync();
        }
    }
}
