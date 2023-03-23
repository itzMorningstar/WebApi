using Entities.SchoolManagement;

namespace WebApi.Dto_s
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int GradeLevel { get; set; }
        public ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}
