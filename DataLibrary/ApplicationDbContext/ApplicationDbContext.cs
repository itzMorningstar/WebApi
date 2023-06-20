using Entities.Accounts;
using Entities.ActivityLogging;
using Entities.DeviceRegistrationEntity;
using Entities.EmployeesEntities;
using Entities.SchoolManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ApplicationDBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base  (options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this is an example of fluent api
            var deviceRegisterConnection = modelBuilder.Entity<DeviceRegisterConnection>();
            deviceRegisterConnection.Property(c => c.UserName).HasColumnType("varchar(50)");
        }

        public DbSet<DeviceRegisterConnection> DeviceRegisterConnections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
