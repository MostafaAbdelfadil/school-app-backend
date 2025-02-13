using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public class Student
    {
        public int StudentId { get; set; }

        [MaxLength(length:15)]
        public required string FirstName { get; set; }

        [MaxLength(length: 15)]
        public required string LastName { get; set; }

        [MaxLength(length: 50)]
        public required string Email { get; set; }

        [MaxLength(length: 15)]
        public string? Phone { get; set; }

        [MaxLength(length: 50)]
        public required string Address { get; set; }

        public required DateOnly DateOfBirth { get; set; }

        public required DateOnly EnrollDate { get; set; }

        public int GenderId { get; set; }

        public LookupGender? Gender { get; set; }

        public int ClassId { get; set; }

        public Class? Class { get; set; }
    }
}
