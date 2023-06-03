using Microsoft.Extensions.DependencyInjection;
using ServicesLibrary.EmployeesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.ExtensionMethod
{
    public static class EmployeeManagement
    {
        public static IServiceCollection EmployeeServiceProvider(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}
