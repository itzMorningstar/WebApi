using Entities.Accounts;

namespace ServicesLibrary.Common
{
    public interface IGeneralService
    {
        string GenerateJwtToken(Account user, bool isPersistence);
    }
}