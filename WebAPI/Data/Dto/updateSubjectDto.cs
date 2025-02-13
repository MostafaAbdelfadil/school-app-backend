using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Dto
{
    public class updateSubjectDto
    {
        [MaxLength(length: 15)]
        public required string SubjectName { get; set; }
        public string? Description { get; set; }

    }
}
