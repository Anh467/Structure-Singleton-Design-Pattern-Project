using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CourseRepository : IRepository<Course>
    {
        public async Task AddAsync(Course entity) => await CourseDAO.Instance.AddAsync(entity);
        

        public async Task DeleteAsync(params object[]? keyValues) => await CourseDAO.Instance.DeleteAsync(keyValues);

        public async Task<Course?> FindAsync(params object[]? keyValues) => await CourseDAO.Instance.FindAsync(keyValues);

        public async Task<IEnumerable<Course>> GetAllAsync() => await CourseDAO.Instance.GetAllAsync();

        public async Task UpdateAsync(Course entity, params object[]? keyValues) => await CourseDAO.Instance.UpdateAsync(entity, keyValues);
    }
}
