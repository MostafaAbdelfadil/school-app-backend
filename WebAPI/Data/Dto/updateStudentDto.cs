using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class updateStudentDto
    {
        [MaxLength(length: 15)]
        public required string FirstName { get; set; }
        [MaxLength(length: 15)]
        public required string LastName { get; set; }

        [MaxLength(length: 50)]
        public required string Email { get; set; }
        [MaxLength(length: 15)]
        public string? Phone { get; set; }
        [MaxLength(length: 50)]
        public required string Address { get; set; }
        public required char? Gender { get; set; }
        public required string? ClassName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required DateOnly EnrollDate { get; set; }
    }
}
