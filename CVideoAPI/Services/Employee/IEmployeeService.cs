using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Employee;
using CVideoAPI.Datasets.Recruitment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Employee
{
    public interface IEmployeeService
    {
        Task<EmployeeDataset> GetById(int id);
        Task<EmployeeDataset> UpdateInfo(EmployeeInfoParam param);
        Task<List<CommonRecruitmentPostDataset>> GetAppliedJobs(int id, RecruitmentPostRequestParam param);
        Task<List<CVDataset>> GetListCVs(int id, CVRequestParam param);
    }
}
