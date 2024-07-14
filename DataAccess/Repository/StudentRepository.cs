using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        public async Task AddAsync(Student entity) => await StudentDAO.Instance.AddAsync(entity); 

        public async Task DeleteAsync(params object[]? keyValues) => await StudentDAO.Instance.DeleteAsync(keyValues); 
        
        public async Task<Student?> FindAsync(params object[]? keyValues) => await StudentDAO.Instance.FindAsync(keyValues);

        public async Task<IEnumerable<Student>> GetAllAsync() => await StudentDAO.Instance.GetAllAsync();

        public async Task UpdateAsync(Student entity, params object[]? keyValues) => await StudentDAO.Instance.UpdateAsync(entity, keyValues);
    }
}
