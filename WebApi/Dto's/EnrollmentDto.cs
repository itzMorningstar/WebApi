using Entities.SchoolManagement;

namespace WebApi.Dto_s
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentDto Student { get; set; }
        public int SectionId { get; set; }
        public SectionDto Section { get; set; }
        public double Grade { get; set; }
    }

}
