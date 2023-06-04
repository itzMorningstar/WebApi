

using Entities;
using Entities.Accounts;
using Entities.DeviceRegistrationEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using ServicesLibrary.AccountServices;

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

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKey = context.HttpContext.Request.Headers[ApplicationConstants.ApiKeyHeaderName];

        if (string.IsNullOrEmpty(apiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key is missing");
            return;
        }

        if (!_apiKeyValidator.IsValid(apiKey,context.HttpContext.Request))
        {
            context.Result = new UnauthorizedObjectResult("API Key is missing");
        }
    }
}
public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IAccountService accountService;

    public ApiKeyValidator(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    public bool IsValid(string apiKey, HttpRequest httpRequest)
    {
        if (string.IsNullOrEmpty(apiKey))
            return false;

         var account  = accountService.GetAccountByApiKeyAsync(apiKey,httpRequest);
            if(account == null)
                return false;
        return true;
    }
}

public interface IApiKeyValidator
{
    bool IsValid(string apiKey, HttpRequest httpRequest);
}