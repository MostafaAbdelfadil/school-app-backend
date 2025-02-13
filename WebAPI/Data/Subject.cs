using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public class Subject
    {
        
        public int SubjectId { get; set; }
        [MaxLength(length:15)]
        public required string SubjectName { get; set; }
        public string? Description { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }

    }
}
