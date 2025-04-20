using Microsoft.AspNetCore.Mvc;
using ExerciseTsadSharat.BL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exercise1TsaSharat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        // GET: api/<InstructorsController>
        [HttpGet]
        public IEnumerable<Instructor> Get()
        {
            Instructor instructor = new Instructor();
            return instructor.Read();
        }

        //// GET api/<InstructorsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<InstructorsController>
        [HttpPost]
        public int Post([FromBody] Instructor instructor)
        {
            return instructor.Insert();
        }



        [HttpGet("GetInstructorById/{id}")]
        public IActionResult GetInstructorById(int id)
        {
            try
            {
                Instructor instructor1 = Instructor.GetInstructorById(id);
                return Ok(instructor1);
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }


        // PUT api/<InstructorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<InstructorsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
