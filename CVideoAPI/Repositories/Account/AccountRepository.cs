using CVideoAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories.Account
{
    public class AccountRepository : GenericRepository<Models.Account>, IAccountRepository
    {
        public AccountRepository(CVideoContext context) : base(context) { }

        public async Task<Models.Account> GetAccountByEmail(string email)
        {
            return await _context.Account.Include(acc => acc.Role).Where(acc => acc.Email == email).FirstOrDefaultAsync();
        }
    }
}
