using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController: ControllerBase
    {
        private readonly SchoolDbContext _dbSchool;

        public TeacherController(SchoolDbContext dbSchool)
        {
            _dbSchool = dbSchool;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> GetAllTeachers()
        {
            var teachers = _dbSchool.Set<Teacher>()
                .Include(t => t.Subject)
                .Select(t => new teacherDto
                {
                    TeacherId = t.TeacherId,
                    Address = t.Address,
                    Phone = t.Phone,
                    Email = t.Email,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    DateOfBirth = t.DateOfBirth,
                    JoiningDate = t.JoiningDate,
                    Subject = t.Subject,
                }).ToList(); 
            return Ok(teachers);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Teacher>> GetById(int id)
        {
            var record = _dbSchool.Set<Teacher>().Include(t => t.Subject)
                .Where(t => t.TeacherId == id)
                .Select(t => new teacherDto
                {
                    TeacherId = t.TeacherId,
                    Address = t.Address,
                    Phone = t.Phone,
                    Email = t.Email,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    DateOfBirth = t.DateOfBirth,
                    JoiningDate = t.JoiningDate,
                    Subject = t.Subject
                }).FirstOrDefault();

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public ActionResult CreateTeacher(addTeacherDto addTeacherDto)
        {
            var newTeacher = new Teacher()
            {
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email,
                Address = addTeacherDto.Address,
                DateOfBirth = addTeacherDto.DateOfBirth,
                JoiningDate = addTeacherDto.JoiningDate,
            };

            if (addTeacherDto.Phone != null)
            {
                newTeacher.Phone = addTeacherDto.Phone;
            }
            if (addTeacherDto.SubjectName != null)
            {
                newTeacher.SubjectId = _dbSchool.Set<Subject>()
                    .Where(su => su.SubjectName == addTeacherDto.SubjectName)
                    .Select(su => su.SubjectId)
                    .FirstOrDefault();
            }

            

            try
            {
                _dbSchool.Set<Teacher>().Add(newTeacher);
                _dbSchool.SaveChanges();
                return Ok(newTeacher);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while adding the teacher." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateTeacher(int id, updateTeacherDto updateTeacherDto)
        {
            var updatedTeacher = _dbSchool.Set<Teacher>().Find(id);
            if (updatedTeacher == null)
            {
                return NotFound();
            }
            updatedTeacher.FirstName = updateTeacherDto.FirstName;
            updatedTeacher.LastName = updateTeacherDto.LastName;
            updatedTeacher.Email = updateTeacherDto.Email;
            updatedTeacher.Address = updateTeacherDto.Address;
            updatedTeacher.DateOfBirth = updateTeacherDto.DateOfBirth;
            updatedTeacher.JoiningDate = updateTeacherDto.JoiningDate;

            if (updateTeacherDto.Phone != null)
            {
                updatedTeacher.Phone = updateTeacherDto.Phone;
            }
            if (updateTeacherDto.SubjectName != null)
            {
                updatedTeacher.SubjectId = _dbSchool.Set<Subject>()
                    .Where(su => su.SubjectName == updateTeacherDto.SubjectName)
                    .Select(su => su.SubjectId)
                    .FirstOrDefault();
            }

           

            try
            {
                _dbSchool.Set<Teacher>().Update(updatedTeacher);
                _dbSchool.SaveChanges();
                return Ok(updatedTeacher);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while updating the teacher." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteTeacher(int id)
        {
            var deletedTeacher = _dbSchool.Set<Teacher>().Find(id);
            if (deletedTeacher == null)
            {
                return NotFound();
            }
            
            

            try
            {
                _dbSchool.Set<Teacher>().Remove(deletedTeacher);
                _dbSchool.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while deleting the teacher." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
