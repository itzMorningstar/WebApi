using Entities.DeviceRegistrationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.ExtensionMethod
{
    public static class DependencyInjections
    {
        public static IServiceCollection ImplementPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebApiDatabase>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(WebApiDatabase).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IDeviceManager, DeviceManager>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IDeviceManager>(provider => provider.GetService<DeviceManager>());

            return services;
        }
    }
}
