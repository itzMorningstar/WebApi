using Entities.Accounts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache memoryCache;

        public AccountService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
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
    }
}
