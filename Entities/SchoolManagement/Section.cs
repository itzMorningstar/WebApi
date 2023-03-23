using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SchoolManagement
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
