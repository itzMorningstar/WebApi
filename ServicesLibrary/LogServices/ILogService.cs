namespace ServicesLibrary.LogServices
{
    public interface ILogService
    {
        void LogError(string message, Exception exception);
    }
}