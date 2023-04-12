using System.ComponentModel.DataAnnotations;
using WebApi.Dto_s;

namespace WebApi.Models.SchoolManagment
{
    public class AddStudentModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public int GradeLevel { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    //    public ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}
