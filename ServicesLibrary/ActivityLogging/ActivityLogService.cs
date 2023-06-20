global using DataLibrary.ApplicationDBContext;
using Entities.Accounts;
using Entities.ActivityLogging;
using Entities.DeviceRegistrationEntity;
using Microsoft.AspNetCore.Http;
using ServicesLibrary.ActivityLogging.Models;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.ActivityLogging
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IGenericRepository<ActivityLog> genericRepository;
        private readonly ApplicationDbContext webApiDatabase;

        public ActivityLogService(IGenericRepository<ActivityLog> genericRepository, ApplicationDbContext webApiDatabase)
        {
            this.genericRepository = genericRepository;
            this.webApiDatabase = webApiDatabase;
        }
        public ActivityLogListVM GetAll(int page = 1, int pageSize = 10)
        {
            var activites = webApiDatabase.ActivityLogs.OrderByDescending(x => x.Timestamp);

            var totalCount = activites.Count();

            var startIndex = (page -1) * pageSize;

            var result = activites.Skip(startIndex).Take(pageSize).OrderByDescending(s => s.Timestamp).ToList();

            return new ActivityLogListVM
            {
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize,
                ActivityLogs = result
            };

        }
        public ActivityLog Get(int id)
        {
            return genericRepository.GetById(id);
        }

        public void Add(ActivityLog ActivityLog)
        {
            genericRepository.Add(ActivityLog);
        }

        public void Add(string description, int type, string username,string? accountGuid, HttpRequest httpRequest)
        {
            var activity = new ActivityLog
            {
                Description = description,
                Timestamp = DateTime.Now,
                IPAddress = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString(),
                AccountGuid = string.IsNullOrEmpty(accountGuid) ? null : new Guid(accountGuid),
                Username = username,
                UserAgent = httpRequest.Headers["User-Agent"],
                TypeId = 5,
            };
            genericRepository.Add(activity);

        }
    }
}
