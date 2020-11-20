using AutoMapper;
using CVideoAPI.Datasets.NewsFeedSection;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.NewsFeed
{
    public class NewsFeedService : INewsFeedService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public NewsFeedService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<NewsFeedSectionDataset>> GetNewsFeedSections(string lang = "vi")
        {
            IEnumerable<NewsFeedSection> list = await _uow.NewsFeedRepository.Get(filter: section => section.Status,
                                                                                orderBy: section => section.OrderBy(section => section.DispOrder));
            IEnumerable<Translation> titles = await _uow.TranslationRepository.Get(filter: trans => trans.Language == lang);
            IEnumerable<NewsFeedSectionDataset> result = _mapper.Map<List<NewsFeedSectionDataset>>(list);
            result = from section in result
                     join title in titles on section.NewsFeedSectionId equals title.NewsFeedSectionId
                     where title.Language == lang
                     select new NewsFeedSectionDataset()
                     {
                         NewsFeedSectionId = section.NewsFeedSectionId,
                         DispOrder = section.DispOrder,
                         Title = title.Text,
                         Url = section.Url
                     };
            return result.ToList();
        }
    }
}
