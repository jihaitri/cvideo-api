using AutoMapper;
using CVideoAPI.Datasets.Company;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Employer
{
    public class EmployerService : IEmployerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public EmployerService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CompanyDataset> GetInfo(int companyId)
        {
            Models.Company company = await _uow.CompanyRepository.GetFirst(filter: company => company.AccountId == companyId, includeProperties: "Account");
            if (company != null)
            {
                return _mapper.Map<CompanyDataset>(company);
            }
            return null;
        }

        public async Task<CompanyStatisticsDataset> GetStatistics(int companyId)
        {
            CompanyStatisticsDataset result = new CompanyStatisticsDataset();
            result.Company = _mapper.Map<CompanyDataset>(await _uow.CompanyRepository.GetById(companyId));
            result.TodayCandidates = (await _uow.AppliedCVRepository.Get(filter: cv => cv.RecruitmentPost.CompanyId == companyId &&
                                                                                        cv.Created.Date == DateTime.UtcNow.Date,
                                                                            includeProperties: "RecruitmentPost")).ToList().Count;
            result.LastMonthCandidates = (await _uow.AppliedCVRepository.Get(filter: cv => cv.RecruitmentPost.CompanyId == companyId &&
                                                                                            cv.Created.Date <= DateTime.UtcNow.Date && cv.Created.Date >= DateTime.UtcNow.AddDays(-30).Date,
                                                                            includeProperties: "RecruitmentPost")).ToList().Count;
            result.TotalCandidates = (await _uow.AppliedCVRepository.Get(filter: cv => cv.RecruitmentPost.CompanyId == companyId, includeProperties: "RecruitmentPost")).ToList().Count;
            result.TotalRecruitmentPosts = (await _uow.RecruitmentRepository.Get(filter: post => post.CompanyId == companyId)).ToList().Count;
            return result;
        }

        public async Task<bool> UpdateInfo(CompanyUpdateParam param)
        {
            Models.Company company = await _uow.CompanyRepository.GetById(param.CompanyId);
            company.Address = param.Address;
            company.Phone = param.Phone;
            company.CompanyName = param.CompanyName;
            company.Description = param.Description;
            _uow.CompanyRepository.Update(company);
            return await _uow.CommitAsync() > 0;
        }

        public async Task ViewApplication(int applicationId)
        {
            Application application = await _uow.AppliedCVRepository.GetById(applicationId);
            application.Viewed = true;
            _uow.AppliedCVRepository.Update(application);
            await _uow.CommitAsync();
        }
    }
}
