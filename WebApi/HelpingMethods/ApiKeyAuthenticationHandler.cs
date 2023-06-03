

using Entities;
using Entities.Accounts;
using Entities.DeviceRegistrationEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute()
        : base(typeof(ApiKeyAuthorizationFilter))
    {
    }
}
public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{

    private readonly IApiKeyValidator _apiKeyValidator;

    public ApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator)
    {
        _apiKeyValidator = apiKeyValidator;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKey = context.HttpContext.Request.Headers[ApplicationConstants.ApiKeyHeaderName];

        if (!_apiKeyValidator.IsValid(apiKey))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
public class ApiKeyValidator : IApiKeyValidator
{
    private readonly WebApiDatabase webApiDatabase;
    private readonly IMemoryCache memoryCache;

    public ApiKeyValidator(WebApiDatabase webApiDatabase, IMemoryCache memoryCache)
    {
        this.webApiDatabase = webApiDatabase;
        this.memoryCache = memoryCache;
    }

    public bool IsValid(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            return false;

        // Check if the account is already in the cache
        if (memoryCache.TryGetValue(apiKey, out Account account))
        {
            // Account found in the cache, return true
            return true;
        }

        // Account not found in the cache, retrieve it from the database
        account = webApiDatabase.Accounts.FirstOrDefault(x => x.ApiKey.ToString() == apiKey);

        if (account != null)
        {
            // Add the account to the cache with a sliding expiration of 30 minutes
            memoryCache.Set(apiKey, account, TimeSpan.FromMinutes(30));
            return true;
        }

        return false;
    }
}

public interface IApiKeyValidator
{
    bool IsValid(string apiKey);
}