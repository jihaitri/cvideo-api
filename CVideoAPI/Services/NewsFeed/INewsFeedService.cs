using CVideoAPI.Datasets.NewsFeedSection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.NewsFeed
{
    public interface INewsFeedService
    {
        Task<List<NewsFeedSectionDataset>> GetNewsFeedSections(string lang = "vi");
    }
}
