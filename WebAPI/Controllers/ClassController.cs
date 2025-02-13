using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassController: ControllerBase
    {
        private readonly SchoolDbContext _dbSchool;

        public ClassController(SchoolDbContext dbSchool)
        {
            _dbSchool = dbSchool;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetAllClasses()
        {
            var classes = _dbSchool.Set<Class>()
                .Include(c => c.Students)
                .Select(c => new classDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Students=c.Students.Select(s => new studentDto
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
                    }).ToList()
                }).ToList();

            return Ok(classes);
        }

        [HttpGet("/classnames")]
        public ActionResult<IEnumerable<string>> GetAllClassNames()
        {
            var classNames = _dbSchool.Set<Class>().Select(c => c.ClassName).ToList();
            return Ok(classNames);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Class>> GetById(int id)
        {
            var record = _dbSchool.Set<Class>()
                .Include(c => c.Students)
                .Where(c => c.ClassId == id)
                .Select(c => new classDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Students = c.Students.Select(s => new studentDto
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
                    }).ToList()
                }).FirstOrDefault();
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public ActionResult CreateClass(addClassDto addClassDto)
        {
            var newClass = new Class()
            {
                ClassName = addClassDto.ClassName
            }; 
            

            try
            {
                _dbSchool.Set<Class>().Add(newClass);
                _dbSchool.SaveChanges();
                return Ok(newClass);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while adding the class." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateClass(int id, updateClassDto updateClassDto)
        {
            var updatedClass = _dbSchool.Set<Class>().Find(id);
            if (updatedClass == null)
            {
                return NotFound();
            }
            updatedClass.ClassName = updateClassDto.ClassName;
            

            try
            {
                _dbSchool.Set<Class>().Update(updatedClass);
                _dbSchool.SaveChanges();
                return Ok(updatedClass);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while updating the class." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteClass(int id)
        {
            var deletedClass = _dbSchool.Set<Class>().Find(id);
            if (deletedClass == null)
            {
                return NotFound();
            }
            
            

            try
            {
                _dbSchool.Set<Class>().Remove(deletedClass);
                _dbSchool.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while deleting the class." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }


        }

    }
}
