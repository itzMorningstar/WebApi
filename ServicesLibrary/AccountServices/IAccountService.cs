using Entities.Accounts;

namespace ServicesLibrary.AccountServices
{
    public interface IAccountService
    {
        Account GetCachedAccount(string apiKey);
    }
}