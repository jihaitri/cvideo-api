using CVideoAPI.Context;

namespace CVideoAPI.Repositories.Company
{
    public class CompanyRepository : GenericRepository<Models.Company>, ICompanyRepository
    {
        public CompanyRepository(CVideoContext context) : base(context) { }
    }
}
