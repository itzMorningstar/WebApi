namespace WebApi.Dto_s
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<SectionDto> Sections { get; set; }
    }
}
