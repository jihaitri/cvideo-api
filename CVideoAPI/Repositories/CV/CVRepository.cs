using CVideoAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories.CV
{
    public class CVRepository : GenericRepository<Models.CV>, ICVRepository
    {
        public CVRepository(CVideoContext context) : base(context) { }

        public async Task<int> GetNumOfCVs(int employeeId)
        {
            return await _context.CV.CountAsync(cv => cv.EmployeeId == employeeId);
        }
    }
}
