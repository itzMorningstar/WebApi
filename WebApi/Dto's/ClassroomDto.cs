namespace WebApi.Dto_s
{
    public class ClassroomDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<SectionDto> Sections { get; set; }
    }
}
