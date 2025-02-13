using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LookupGenderController: ControllerBase
    {
        private readonly SchoolDbContext _dbSchool;

        public LookupGenderController(SchoolDbContext dbSchool)
        {
            _dbSchool = dbSchool;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LookupGender>> GetAllGenders()
        {
            var genders = _dbSchool.Set<LookupGender>()
                .Include(g=>g.GenderStudents)
                .Select(g=>new genderDto
                {
                    GenderId = g.GenderId,
                    GenderName = g.GenderName,
                    GenderStudents = g.GenderStudents.Select(s=>new studentDto
                    {
                        StudentId = s.StudentId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Phone = s.Phone,
                        Address = s.Address,
                        Email = s.Email,
                        DateOfBirth = s.DateOfBirth,
                        EnrollDate = s.EnrollDate,
                        Gender = s.Gender,
                        Class = s.Class,
                    }).ToList()

                }).ToList();
            return Ok(genders);
        }

        [HttpGet("/gendernames")]
        public ActionResult<IEnumerable<char>> GetAllGenderNames()
        {
            var genderNames = _dbSchool.Set<LookupGender>().Select(g => g.GenderName).ToList();
            return Ok(genderNames);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<LookupGender>> GetById(int id)
        {
            var record = _dbSchool.Set<LookupGender>()
                .Include(g => g.GenderStudents)
                .Where(g => g.GenderId == id)
                .Select(g => new genderDto
                {
                    GenderId = g.GenderId,
                    GenderName = g.GenderName,
                    GenderStudents = g.GenderStudents.Select(s => new studentDto
                    {
                        StudentId = s.StudentId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Phone = s.Phone,
                        Address = s.Address,
                        Email = s.Email,
                        DateOfBirth = s.DateOfBirth,
                        EnrollDate = s.EnrollDate,
                        Gender = s.Gender,
                        Class = s.Class,
                    }).ToList()

                }).FirstOrDefault();
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public ActionResult CreateLookupGender(addLookupGenderDto addLookupGenderDto)
        {
            var newLookupGender = new LookupGender()
            {
                GenderId = addLookupGenderDto.GenderId,
                GenderName = addLookupGenderDto.GenderName
            }; 
            

            try
            {
                _dbSchool.Set<LookupGender>().Add(newLookupGender);
                _dbSchool.SaveChanges();
                return Ok(newLookupGender);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while adding the gender." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateLookupGender(int id, updateLookupGenderDto updateLookupGenderDto)
        {
            var updatedLookupGender = _dbSchool.Set<LookupGender>().Find(id);
            if (updatedLookupGender == null)
            {
                return NotFound();
            }
            updatedLookupGender.GenderId = updateLookupGenderDto.GenderId;
            updatedLookupGender.GenderName = updateLookupGenderDto.GenderName;

            
            try
            {
                _dbSchool.Set<LookupGender>().Update(updatedLookupGender);
                _dbSchool.SaveChanges();
                return Ok(updatedLookupGender);
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while updating the gender." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteLookupGender(int id)
        {
            var deletedLookupGender = _dbSchool.Set<LookupGender>().Find(id);
            if (deletedLookupGender == null)
            {
                return NotFound();
            }
            
            
            try
            {
                _dbSchool.Set<LookupGender>().Remove(deletedLookupGender);
                _dbSchool.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                /*if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    return BadRequest(new {message= "Email or Phone number already exists." });
                }*/
                return StatusCode(500, new { message = "An error occurred while deleting the gender." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
