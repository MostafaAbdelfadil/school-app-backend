using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebAPI.Data
{
    public class Class
    {
      
        public int ClassId { get; set; }
        [MaxLength(length: 15)]
        public required string ClassName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
