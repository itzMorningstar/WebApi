using Entities.ActivityLogging;
using Microsoft.AspNetCore.Http;
using ServicesLibrary.ActivityLogging.Models;

namespace ServicesLibrary.ActivityLogging
{
    public interface IActivityLogService
    {
        void Add(ActivityLog ActivityLog);
        void Add(string description,  int type, string username, string? accountGuid, HttpRequest httpRequest);
        ActivityLog Get(int id);
        ActivityLogListVM GetAll(int page =1,int pageSize =10);
    }
}