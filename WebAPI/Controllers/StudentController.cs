using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController: ControllerBase
    {
        private readonly SchoolDbContext _dbSchool;

        public StudentController(SchoolDbContext dbSchool)
        {
            _dbSchool = dbSchool;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = _dbSchool.Set<Student>()
                .Include(s=>s.Gender)
                .Include(s=>s.Class)
                .Select(s=>new studentDto
                {
                    StudentId = s.StudentId,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DateOfBirth = s.DateOfBirth,
                    EnrollDate = s.EnrollDate,
                    Gender = s.Gender,
                    Class = s.Class,
                }).ToList();

            return Ok(students);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Student>> GetById(int id)
        {
            var record = _dbSchool.Set<Student>()
                .Include(s => s.Gender)
                .Include(s => s.Class)
                .Where(s => s.StudentId == id)
                .Select(s => new studentDto
                {
                    StudentId = s.StudentId,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DateOfBirth = s.DateOfBirth,
                    EnrollDate = s.EnrollDate,
                    Gender = s.Gender,
                    Class = s.Class,
                }).FirstOrDefault();
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public ActionResult CreateStudent(addStudentDto addStudentDto)
        {
            var newStudent = new Student()
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                Email = addStudentDto.Email,
                Address = addStudentDto.Address,
                DateOfBirth = addStudentDto.DateOfBirth,
                EnrollDate = addStudentDto.EnrollDate
            };

            if (addStudentDto.Phone != null)
            {
                newStudent.Phone = addStudentDto.Phone;
            }

            if (addStudentDto.Gender != null)
            {
                newStudent.GenderId = _dbSchool.Set<LookupGender>()
                    .Where(g => g.GenderName == addStudentDto.Gender)
                    .Select(g=>g.GenderId)
                    .FirstOrDefault();
            }
            if (addStudentDto.ClassName != null)
            {
                newStudent.ClassId = _dbSchool.Set<Class>()
                    .Where(c => c.ClassName == addStudentDto.ClassName)
                    .Select(c => c.ClassId)
                    .FirstOrDefault();
            }

            try
            {
                _dbSchool.Set<Student>().Add(newStudent);
                _dbSchool.SaveChanges();
                return Ok(newStudent);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new {message = "An error occurred while adding the student." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            

        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateStudent(int id, updateStudentDto updateStudentDto)
        {
            var updatedStudent = _dbSchool.Set<Student>().Find(id);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            updatedStudent.FirstName = updateStudentDto.FirstName;
            updatedStudent.LastName = updateStudentDto.LastName;
            updatedStudent.Email = updateStudentDto.Email;
            updatedStudent.Address = updateStudentDto.Address;
            updatedStudent.DateOfBirth = updateStudentDto.DateOfBirth;
            updatedStudent.EnrollDate = updateStudentDto.EnrollDate;

            if (updateStudentDto.Phone != null)
            {
                updatedStudent.Phone = updateStudentDto.Phone;
            }
            if (updateStudentDto.Gender != null)
            {
                updatedStudent.GenderId = _dbSchool.Set<LookupGender>()
                    .Where(g => g.GenderName == updateStudentDto.Gender)
                    .Select(g => g.GenderId)
                    .FirstOrDefault();
            }
            if (updateStudentDto.ClassName != null)
            {
                updatedStudent.ClassId = _dbSchool.Set<Class>()
                    .Where(c => c.ClassName == updateStudentDto.ClassName)
                    .Select(c => c.ClassId)
                    .FirstOrDefault();
            }

            

            try
            {
                _dbSchool.Set<Student>().Update(updatedStudent);
                _dbSchool.SaveChanges();
                return Ok(updatedStudent);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while updating the student." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            var deletedStudent = _dbSchool.Set<Student>().Find(id);
            if (deletedStudent == null)
            {
                return NotFound();
            }
            
            

            try
            {
                _dbSchool.Set<Student>().Remove(deletedStudent);
                _dbSchool.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while deleting the student." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
