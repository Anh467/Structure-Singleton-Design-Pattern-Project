using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _repository;
        public StudentsController(IRepository<Student> repository) {
            this._repository = repository;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var temp = await _repository.FindAsync(id);
            if (temp == null) return NotFound();
            return Ok(temp);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            await _repository.AddAsync(student);
            return NoContent();
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            var temp = await _repository.FindAsync(id);
            if (temp == null) return NotFound();

            await _repository.UpdateAsync(student, id);
            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var temp = await _repository.FindAsync(id);
            if (temp == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
