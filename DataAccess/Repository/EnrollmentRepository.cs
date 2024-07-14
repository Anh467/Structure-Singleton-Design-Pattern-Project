using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EnrollmentRepository : IRepository<Enrollment>
    {
        public async Task AddAsync(Enrollment entity) => await EnrollmentDAO.Instance.AddAsync(entity);

        public async Task DeleteAsync(params object[]? keyValues) => await EnrollmentDAO.Instance.DeleteAsync(keyValues);

        public async Task<Enrollment?> FindAsync(params object[]? keyValues) => await EnrollmentDAO.Instance.FindAsync(keyValues);

        public async Task<IEnumerable<Enrollment>> GetAllAsync() => await EnrollmentDAO.Instance.GetAllAsync();

        public async Task UpdateAsync(Enrollment entity, params object[]? keyValues) => await EnrollmentDAO.Instance.UpdateAsync(entity, keyValues);
    }
}
