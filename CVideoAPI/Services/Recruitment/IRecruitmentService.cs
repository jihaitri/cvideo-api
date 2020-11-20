using CVideoAPI.Datasets;
using CVideoAPI.Datasets.Application;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Recruitment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Recruitment
{
    public interface IRecruitmentService
    {
        Task<List<CommonRecruitmentPostDataset>> GetListRecruitmentPost(RecruitmentPostRequestParam param);
        Task<CommonRecruitmentPostDataset> GetRecruitmentPostById(int id, string lang = "vi");
        Task<CommonRecruitmentPostDataset> GetRecruitmentPostByName(int compannyId, string title);
        Task<List<PostStaticsDataset>> GetPostStatisticsList(int companyId, CommonParam param);
        Task<PostStaticsDataset> GetPostStatisticsById(int id, string lang = "vi");
        Task<CommonRecruitmentPostDataset> InsertRecruitmentPost(NewRecruitmentPostParam param);
        Task<bool> UpdateRecruitmentPost(RecruitmentPostToUpdateParam param);
        Task<List<ApplicationDataset>> GetAllAppliedCVs(int companyId, ApplicationParam param);
        Task<List<ApplicationDataset>> GetAppliedCVs(int postId, ApplicationParam param);
        Task<ApplicationDataset> GetAppliedCV(int applicationId);
        Task<bool> ApplyCV(int postId, int userId, int cvId);
        Task<bool> IsUserApplied(int postId, int userId);
    }
}
