using Entities.ActivityLogging;
using Entities.DeviceRegistrationEntity;
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
        private readonly WebApiDatabase webApiDatabase;

        public ActivityLogService(IGenericRepository<ActivityLog> genericRepository, WebApiDatabase webApiDatabase)
        {
            this.genericRepository = genericRepository;
            this.webApiDatabase = webApiDatabase;
        }
        public List<ActivityLog> GetAll()
        {
            return webApiDatabase.ActivityLogs.ToList();

        }
        public ActivityLog Get(int id)
        {
            return genericRepository.GetById(id);
        }

        public void Add(ActivityLog ActivityLog)
        {
            genericRepository.Add(ActivityLog);
        }
    }
}
