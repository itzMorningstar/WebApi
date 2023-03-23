using Entities.SchoolManagement;

namespace WebApi.Dto_s
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public TeacherDto Teacher { get; set; }
        public int ClassroomId { get; set; }
        public ClassroomDto Classroom { get; set; }
        public ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}
