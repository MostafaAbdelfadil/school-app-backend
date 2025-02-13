using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Dto
{
    public class classDto
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public List<studentDto>? Students { get; set; }

    }
}
