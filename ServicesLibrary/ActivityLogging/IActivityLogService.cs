using Entities.ActivityLogging;
using Microsoft.AspNetCore.Http;

namespace ServicesLibrary.ActivityLogging
{
    public interface IActivityLogService
    {
        void Add(ActivityLog ActivityLog);
        void Add(string description,  int type, string username, string? accountGuid, HttpRequest httpRequest);
        ActivityLog Get(int id);
        List<ActivityLog> GetAll();
    }
}