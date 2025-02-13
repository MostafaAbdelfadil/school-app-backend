using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Dto
{
    public class studentDto
    {
        public int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required string Address { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required DateOnly EnrollDate { get; set; }
        public LookupGender? Gender { get; set; }
        public Class? Class { get; set; }
    }

}
