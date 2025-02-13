namespace WebAPI.Data.Dto
{
    public class genderDto
    {
        public int GenderId { get; set; }
        public char? GenderName { get; set; }
        public List<studentDto>? GenderStudents { get; set; }

    }
}
