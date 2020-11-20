using AutoMapper;
using CVideoAPI.Datasets.Video;
using CVideoAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Video
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public VideoService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<VideoDataset> GetVideoById(int id)
        {
            return _mapper.Map<VideoDataset>(await _uow.VideoRepository.GetFirst(filter: video => video.VideoId == id, includeProperties: "Section,VideoStyle"));
        }

        public async Task<VideoDataset> InsertVideo(VideoUploadParam upload)
        {
            Models.Video video = new Models.Video()
            {
                SectionId = upload.SectionId,
                StyleId = upload.StyleId,
                AspectRatio = upload.AspectRatio,
                CoverUrl = upload.CoverUrl,
                ThumbUrl = upload.ThumbUrl,
                VideoUrl = upload.VideoUrl
            };
            _uow.VideoRepository.Insert(video);
            if (await _uow.CommitAsync() > 0)
            {
                return await GetVideoById(video.VideoId);
            }
            return null;
        }
        public async Task<List<VideoStyleDataset>> GetStyles()
        {
            return _mapper.Map<List<VideoStyleDataset>>(await _uow.StyleRepository.Get());
        }

        public async Task<List<VideoDataset>> GetVideos(int? cvId, int? sectionId, int? styleId)
        {
            Expression<Func<Models.Video, bool>> filter = video => (cvId == null || video.Section.CVId == cvId) &&
                                                                    (sectionId == null || video.SectionId == sectionId) &&
                                                                    (styleId == null || video.StyleId == styleId);
            IEnumerable<Models.Video> videos = await _uow.VideoRepository.Get(filter: filter,
                                                                                includeProperties: "Section,VideoStyle",
                                                                                orderBy: video => video.OrderBy(video => video.Created));
            return _mapper.Map<List<VideoDataset>>(videos);
        }

        public async Task<bool> DeleteVideo(int id)
        {
            _uow.VideoRepository.Delete(id);
            return await _uow.CommitAsync() > 0;
        }
    }
}
