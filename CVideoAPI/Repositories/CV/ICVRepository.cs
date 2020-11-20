using System.Threading.Tasks;

namespace CVideoAPI.Repositories.CV
{
    public interface ICVRepository : IGenericRepository<Models.CV>
    {
        Task<int> GetNumOfCVs(int employeeId);
    }
}
