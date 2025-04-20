using Microsoft.AspNetCore.Mvc;
using ExerciseTsadSharat.BL;
using Microsoft.VisualBasic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exercise1TsaSharat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        // GET: api/<CoursesController>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            Course course = new Course();
            return course.Read();
        }

        //// GET api/<CoursesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CoursesController>
        [HttpPost]
        public int Post([FromBody] Course course)
        {
            return course.Insert();
        }

        // PUT api/<CoursesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course updatedCourse)
        {
            if (id != updatedCourse.Id)
                return BadRequest(new { error = "Course ID in the URL does not match the ID in the request body" });

            try
            {
                Course.UpdateCourse(updatedCourse);
                return Ok(new { message = "Course updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {  
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //// DELETE api/<CoursesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        [HttpGet("GetCourseById/{id}")]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                Course course1 = Course.GetCourseById(id);
                return Ok(course1);
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }

        [HttpPost("InsertNewCourse")]
        public IActionResult InsertNewCourse([FromBody] Course newCourse)
        {
            DateTime dateAndTime = DateTime.Now;
            newCourse.LastUpdate = dateAndTime.ToString("dd/MM/yyyy");
            bool flag = Course.CheckInstructorExists(newCourse.InstructorId);


            if (flag && newCourse.Insert() == 1)
            {
                return Ok(new { Message = "Course has been added successfully" });
            }
            else
            {
                return NotFound(new { Message = "A course already exists with this id/ Instructor wasn't found" });
            }
        }

        [HttpGet("GetCoursesByInstructorId/{instructorId}")]
        public IActionResult GetCoursesByInstructorId(int instructorId)
        {
            try
            {
                List<Course> courses = Course.GetCoursesByInstructorId(instructorId);
                return Ok(courses);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPut("UpdateCourseIsActiveAndTitle/Course Id/{courseId}/Is Active/{isActive}/Title/{title}")]
        public IActionResult UpdateCourseIsActiveAndTitle(int courseId, bool isActive, string title)
        {
            try
            {
                Course.changeIsActiveAndTitle(courseId, isActive, title);
                return Ok(new {message = "Course with id: " +courseId+" isActive status and name has been updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetTop5Courses")]
        public object GetTop5Courses()
        {
            Course course = new Course();
            return course.GetTop5CoursesByUsersRegistered();
        }


        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] List<IFormFile> files)
        {
            List<string> imageLinks = new List<string>();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploadedFiles");

            // Ensure the directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Allowed image MIME types and extensions
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };

            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                    var mimeType = formFile.ContentType;

                    // Check if the file has a valid extension and MIME type
                    if (string.IsNullOrEmpty(extension) ||
                        !permittedExtensions.Contains(extension) ||
                        !permittedMimeTypes.Contains(mimeType))
                    {
                        return BadRequest("Invalid file type.");
                    }

                    // Generate a unique file name if a file with the same name exists
                    var fileName = formFile.FileName;
                    var filePath = Path.Combine(path, fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        fileName = $"{Path.GetFileNameWithoutExtension(formFile.FileName)}_{DateTime.Now.Ticks}{extension}";
                        filePath = Path.Combine(path, fileName);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    imageLinks.Add(fileName);
                }
            }

            return Ok(imageLinks);
        }

        [HttpDelete("DeleteImage/File Name/{fileName}")]
        public IActionResult DeleteImage(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploadedFiles", fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Ok(new { Message = $"Image {fileName} deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = $"Image {fileName} not found" });
            }
        }

    }
}