using System.Threading.Tasks;

namespace CVideoAPI.Repositories.Video
{
    public interface IVideoRepository : IGenericRepository<Models.Video>
    {
        Task<int> GetNumberOfVideos(int employeeId);
    }
}
