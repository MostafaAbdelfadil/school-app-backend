using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Dto
{
    public class subjectDto
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public List<teacherDto>? Teachers { get; set; }

    }
}
