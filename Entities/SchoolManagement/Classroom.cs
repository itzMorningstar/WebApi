using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SchoolManagement
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<Section> Sections { get; set; }
    }

}
