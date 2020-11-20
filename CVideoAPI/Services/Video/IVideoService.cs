using CVideoAPI.Datasets.Video;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Video
{
    public interface IVideoService
    {
        Task<List<VideoDataset>> GetVideos(int? cvId, int? sectionId, int? styleId);
        Task<VideoDataset> GetVideoById(int id);
        Task<VideoDataset> InsertVideo(VideoUploadParam upload);
        Task<bool> DeleteVideo(int id);
        Task<List<VideoStyleDataset>> GetStyles();
    }
}
