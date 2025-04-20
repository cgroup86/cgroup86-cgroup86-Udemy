using Microsoft.AspNetCore.Mvc;
using ExerciseTsadSharat.BL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExerciseTsadSharat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user = new User();
            return user.Read();
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                user.Register();
                return Ok(new { message = "User registered successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                user.Login();
                return Ok(new { id = user.Id, message = "Logged in successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetMyCourses")] // this uses the QueryString
        public IEnumerable<Course> GetMyCourses(int id)
        {
            User user = new User();
            return user.ReadMyCourses(id);
        }

        [HttpPost("PostMyCourses")] // QueryString
        public bool PostMyCourses([FromBody] Course course, int id)
        {
            User user = new User();
            return user.InsertCourse(course, id);
        }


        [HttpGet("SearchByDuration")] // this uses the QueryString
        public IEnumerable<Course> GetByDurationRange(float minDuration, float maxDuration,int id)
        {
            User user = new User();
            return user.GetByDurationRange(minDuration, maxDuration,id);
        }


        [HttpGet("searchByRating/Minimum rating/{minRating}/Maximum rating/{maxRating}/Id/{id}")] // this uses resource routing
        public IEnumerable<Course> GetByRatingRange(float minRating, float maxRating,int id)
        {
            User user = new User();
            return user.GetByRatingRange(minRating, maxRating, id);
        }


        [HttpDelete("DeleteCourseById/CourseId/{courseId}/Id/{userId}")]
        public IActionResult DeleteById(int courseId, int userId)
        {
            try
            {
                User user = new User();
                user.DeleteById(courseId, userId);
                return Ok(new { message = "Course with id: " + courseId + " has been deleted successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //------------------------------------------------------------------------------------------

        //// GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UsersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
