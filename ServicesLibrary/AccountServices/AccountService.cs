using Entities.Accounts;
using Entities.DeviceRegistrationEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServicesLibrary.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGenericRepository<Account> accountRepository;
        private readonly WebApiDatabase webApiDatabase;

        public AccountService(IMemoryCache memoryCache, IGenericRepository<Account> accountRepository ,WebApiDatabase webApiDatabase)
        {
            this.memoryCache = memoryCache;
            this.accountRepository = accountRepository;
            this.webApiDatabase = webApiDatabase;
        }

        public Account GetCachedAccount(string apiKey)
        {
            // Try to get the account from the cache using the API key as the cache key
            if (memoryCache.TryGetValue(apiKey, out Account account))
            {
                // Account found in the cache, return it
                return account;
            }
            // Account not found in the cache
            return null;
        }

         public async Task<Account?> GetAccountByApiKeyAsync(string apiKey, HttpRequest httpRequest)
        {
            // Try to get the account from the cache using the API key as the cache key
            if (memoryCache.TryGetValue(apiKey, out Account account))
            {
                // Account found in the cache, return it
                return account;
            }
            // Account not found in the cache, get it from the database
            account =  webApiDatabase.Accounts.Where(a => a.ApiKey.ToString() == apiKey).FirstOrDefault();

            if (account == null)
            {
                return null;
            }
            //update the user  last login details

            var userAgent = httpRequest.Headers["User-Agent"].ToString();
            // Extract last login OS from User-Agent header
            var lastLoginOS = ParseLastLoginOS(userAgent);
            // Update account with last login OS
            account.LastLoginOS = lastLoginOS;

            account.UpdateOn = DateTime.Now;
            account.LastLoginIP = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
            account.LastLoginBrowser = userAgent.Substring(0,49);
            account.LastLoginUserAgent = userAgent.Substring(0,99);
            accountRepository.Update(account);

            // Add the account to the cache
            memoryCache.Set(apiKey, account, TimeSpan.FromHours(2));
            // Return the account

            return account;
        }

        public void updateAccount(Account account)
        {
            accountRepository.Update(account);
        }

        private string ParseLastLoginOS(string userAgent)
        {
            // Add your parsing logic here to extract the last login OS from the User-Agent header

            // Example: Extracting the operating system information from User-Agent using regular expressions
            var regex = new Regex(@"\((.*?)\)");
            var matches = regex.Matches(userAgent);
            if (matches.Count > 0)
            {
                var osInfo = matches[matches.Count - 1].Value;
                return osInfo.Trim('(', ')');
            }

            return string.Empty;
        }
    }
}
