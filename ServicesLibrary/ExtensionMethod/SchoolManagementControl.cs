using Microsoft.Extensions.DependencyInjection;
using ServicesLibrary.SchoolManagementServices.Enrollmentservices;
using ServicesLibrary.SchoolManagementServices.StudentServices;
using ServicesLibrary.SchoolManagementServices.TeacherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.ExtensionMethod
{
    public static class SchoolManagementControl
    {
        public static IServiceCollection SchoolServicesProvider(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            
            return services;
        }
    }
}
