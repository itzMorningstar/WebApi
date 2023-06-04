using Entities.Accounts;
using Microsoft.AspNetCore.Http;

namespace ServicesLibrary.AccountServices
{
    public interface IAccountService
    {
        Account GetCachedAccount(string apiKey);

        Task<Account?> GetAccountByApiKeyAsync(string apiKey, HttpRequest httpRequest);

        void updateAccount(Account account);

    }
}