using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IRepository<Course> _repository;
        public CoursesController(IRepository<Course> repository) {
            this._repository = repository;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var temp = await _repository.FindAsync(id);
            if (temp == null) return NotFound();
            return Ok(temp);
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            await _repository.AddAsync(course);
            return NoContent();
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Course course)
        {
            var temp = await  _repository.FindAsync(id);
            if (temp == null) return NotFound();

            await _repository.UpdateAsync(course, id);
            return NoContent();
        }

        // DELETE api/<CoursesController>/5
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
