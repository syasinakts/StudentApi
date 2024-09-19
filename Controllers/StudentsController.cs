using StudentApi.Data;
using StudentApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = ApplicationContext.Students;
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneStudent([FromRoute(Name = "id")] int id)
        {
            var student = ApplicationContext.Students
                .SingleOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Student with id:{id} could not be found."
                });
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostOneStudent([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student data is null");

            ApplicationContext.Students.Add(student);
            return StatusCode(StatusCodes.Status201Created, student);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneStudent([FromRoute(Name = "id")] int id, [FromBody] Student student)
        {
            var entity = ApplicationContext.Students
                .SingleOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Student with id:{id} could not be found."
                });

            if (id != student.Id)
                return BadRequest("Student ID mismatch");

            // Updating the entity directly rather than removing and adding again
            entity.FirstName = student.FirstName;
            entity.LastName = student.LastName;
            entity.Age = student.Age;
            entity.Gender = student.Gender;
            entity.Grade = student.Grade;

            return Ok(entity);
        }

        [HttpDelete]
        public IActionResult DeleteAllStudents()
        {
            ApplicationContext.Students.Clear();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneStudent([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext.Students
                .SingleOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Student with id:{id} could not be found."
                });

            ApplicationContext.Students.Remove(entity);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneStudent([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Student> studentPatch)
        {
            var entity = ApplicationContext.Students
                .SingleOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Student with id:{id} could not be found."
                });

            studentPatch.ApplyTo(entity, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(entity);
        }
    }
}
