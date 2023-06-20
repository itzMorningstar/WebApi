namespace ServicesLibrary.HttpClientServices
{
    public interface IWebRequestHelper
    {
        Task<object> GetRequestAsync(string url, Dictionary<string, string> headers);
    }
}