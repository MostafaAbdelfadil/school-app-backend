using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public class LookupGender
    {
        
        public int GenderId { get; set; }
        public required char GenderName { get; set; }
        public ICollection<Student>? GenderStudents { get; set; }
    }
}
