using Entities.ActivityLogging;

namespace ServicesLibrary.ActivityLogging
{
    public interface IActivityLogService
    {
        void Add(ActivityLog ActivityLog);
        ActivityLog Get(int id);
        List<ActivityLog> GetAll();
    }
}