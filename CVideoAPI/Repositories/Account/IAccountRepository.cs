using System.Threading.Tasks;

namespace CVideoAPI.Repositories.Account
{
    public interface IAccountRepository : IGenericRepository<Models.Account>
    {
        Task<Models.Account> GetAccountByEmail(string email);
    }
}
