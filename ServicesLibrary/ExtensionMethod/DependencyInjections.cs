﻿using Entities.DeviceRegistrationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesLibrary.AccountServices;
using ServicesLibrary.ActivityLogging;
using ServicesLibrary.Common;
using ServicesLibrary.GenericRepositories;
using ServicesLibrary.HttpClientServices;
using ServicesLibrary.LogServices;
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
            //  No need for this becuase we are configuring our db context in the main web project 

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            //    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddScoped<IDeviceManager, DeviceManager>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IWebRequestHelper, WebRequestHelper>();
            services.AddScoped<IGeneralService, GeneralService>();
            //services.AddScoped<IDeviceManager>(provider => provider.GetService<DeviceManager>());

            return services;
        }
    }
}
