using CVideoAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories.Video
{
    public class VideoRepository : GenericRepository<Models.Video>, IVideoRepository
    {
        public VideoRepository(CVideoContext context) : base(context) { }

        public async Task<int> GetNumberOfVideos(int employeeId)
        {
            return await _context.Video
                                .Include(video => video.Section)
                                    .ThenInclude(section => section.CV)
                                .CountAsync(video => video.Section.CV.EmployeeId == employeeId);
        }
    }
}
