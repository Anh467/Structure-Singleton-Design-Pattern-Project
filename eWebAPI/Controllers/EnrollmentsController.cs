using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IRepository<Enrollment> _repository;
        public EnrollmentsController(IRepository<Enrollment> repository)
        {
            this._repository = repository;
        }

        // GET: api/<EnrollmentsController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET api/<EnrollmentsController>/5
        [HttpGet("{studentid}/{courseid}")]
        public async Task<IActionResult> Get(int studentid, int courseid)
        {
            var temp = await _repository.FindAsync(studentid, courseid);
            if (temp == null) return NotFound();
            return Ok(temp);
        }

        // POST api/<EnrollmentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Enrollment student)
        {
            await _repository.AddAsync(student);
            return NoContent();
        }

        // PUT api/<EnrollmentsController>/5
        [HttpPut("{studentid}/{courseid}")]
        public async Task<IActionResult> Put(int studentid, int courseid, [FromBody] Enrollment student)
        {
            var temp = await _repository.FindAsync(studentid, courseid);
            if (temp == null) return NotFound();

            await _repository.UpdateAsync(student, studentid, courseid);
            return NoContent();
        }

        // DELETE api/<EnrollmentsController>/5
        [HttpDelete("{studentid}/{courseid}")]
        public async Task<IActionResult> Delete(int studentid, int courseid)
        {
            var temp = await _repository.FindAsync(studentid, courseid);
            if (temp == null) return NotFound();

            await _repository.DeleteAsync(studentid, courseid);
            return NoContent();
        }
    }
}
