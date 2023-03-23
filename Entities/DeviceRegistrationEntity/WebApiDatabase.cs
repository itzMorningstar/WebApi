using Entities.SchoolManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DeviceRegistrationEntity
{
    public class WebApiDatabase : DbContext
    {
        public WebApiDatabase(DbContextOptions<WebApiDatabase> options)
            :base(options)
        {}
        public DbSet<DeviceRegisterConnection> DeviceRegisterConnections { get; set; }
        public DbSet<Student> Students{ get; set; }
        public DbSet<Classroom> Classrooms{ get; set; }
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<Section> Sections{ get; set; }
        public DbSet<Enrollment> Enrollments{ get; set; }

    }
}
