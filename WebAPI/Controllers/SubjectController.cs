using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController: ControllerBase
    {
        private readonly SchoolDbContext _dbSchool;

        public SubjectController(SchoolDbContext dbSchool)
        {
            _dbSchool = dbSchool;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Subject>> GetAllSubjects()
        {
            var subjects = _dbSchool.Set<Subject>()
                .Include(su=>su.Teachers)
                .Select(su=>new subjectDto
                {
                    SubjectId = su.SubjectId,
                    SubjectName = su.SubjectName,
                    Description = su.Description,
                    Teachers = su.Teachers.Select(t=>new teacherDto
                    {
                        TeacherId = t.TeacherId,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Phone = t.Phone,
                        Email = t.Email,
                        Address = t.Address,
                        DateOfBirth = t.DateOfBirth,
                        JoiningDate = t.JoiningDate,
                        Subject = t.Subject
                    }).ToList()
                }).ToList();
            return Ok(subjects);
        }

        [HttpGet("/subjectnames")]
        public ActionResult<IEnumerable<string>> GetAllSubjectNames()
        {
            var subjectNames = _dbSchool.Set<Subject>().Select(su => su.SubjectName).ToList();
            return Ok(subjectNames);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Subject>> GetById(int id)
        {
            var record = _dbSchool.Set<Subject>()
                .Include(su => su.Teachers)
                .Where(su => su.SubjectId == id)
                .Select(su => new subjectDto
                {
                    SubjectId = su.SubjectId,
                    SubjectName = su.SubjectName,
                    Description = su.Description,
                    Teachers = su.Teachers.Select(t => new teacherDto
                    {
                        TeacherId = t.TeacherId,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Phone = t.Phone,
                        Email = t.Email,
                        Address = t.Address,
                        DateOfBirth = t.DateOfBirth,
                        JoiningDate = t.JoiningDate,
                        Subject = t.Subject,

                    }).ToList()
                }).FirstOrDefault();
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public ActionResult CreateSubject(addSubjectDto addSubjectDto)
        {
            var newSubject = new Subject()
            {
                SubjectName = addSubjectDto.SubjectName,
                Description = addSubjectDto.Description
            };
            
            

            try
            {
                _dbSchool.Set<Subject>().Add(newSubject);
                _dbSchool.SaveChanges();
                return Ok(newSubject);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while adding the subject." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateSubject(int id, updateSubjectDto updateSubjectDto)
        {
            var updatedSubject = _dbSchool.Set<Subject>().Find(id);
            if (updatedSubject == null)
            {
                return NotFound();
            }
            updatedSubject.SubjectName = updateSubjectDto.SubjectName;
            updatedSubject.Description = updateSubjectDto.Description;

            

            try
            {
                _dbSchool.Set<Subject>().Update(updatedSubject);
                _dbSchool.SaveChanges();
                return Ok(updatedSubject);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while updating the subject." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteSubject(int id)
        {
            var deletedSubject = _dbSchool.Set<Subject>().Find(id);
            if (deletedSubject == null)
            {
                return NotFound();
            }
            
            

            try
            {
                _dbSchool.Set<Subject>().Remove(deletedSubject);
                _dbSchool.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while deleting the subject." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
