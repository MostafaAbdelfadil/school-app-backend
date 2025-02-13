using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public class Teacher
    {
        public int TeacherId { get; set; }

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

        public required DateOnly JoiningDate { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
