using CVideoAPI.Datasets.Company;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Employer
{
    public interface IEmployerService
    {
        Task<CompanyDataset> GetInfo(int companyId);
        Task<CompanyStatisticsDataset> GetStatistics(int companyId);
        Task<bool> UpdateInfo(CompanyUpdateParam param);
        Task ViewApplication(int applicationId);
    }
}
