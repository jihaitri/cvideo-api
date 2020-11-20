using AutoMapper;
using CVideoAPI.Datasets.CV;
using CVideoAPI.Datasets.Employee;
using CVideoAPI.Datasets.Recruitment;
using CVideoAPI.Models;
using CVideoAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<EmployeeDataset> GetById(int id)
        {
            Models.Employee employee = await _uow.EmployeeRepository.GetFirst(filter: emp => emp.AccountId == id, includeProperties: "Account");
            EmployeeDataset result = null;
            if (employee != null)
            {
                int numOfCVs = await _uow.CVRepository.GetNumOfCVs(id);
                int numOfVideos = await _uow.VideoRepository.GetNumberOfVideos(id);
                result = _mapper.Map<EmployeeDataset>(employee);
                result.NumOfCVs = numOfCVs;
                result.NumOfVideos = numOfVideos;
            }
            return result;
        }
        public async Task<List<CommonRecruitmentPostDataset>> GetAppliedJobs(int id, RecruitmentPostRequestParam param)
        {
            Expression<Func<Models.Application, bool>> filter = cv => (cv.RecruitmentPost.Company.CompanyName.Contains(param.CompanyName)
                                                                                && cv.RecruitmentPost.Location.Contains(param.Location)
                                                                                && (param.CompanyId == 0 || cv.RecruitmentPost.CompanyId == param.CompanyId)
                                                                                && (param.MajorId == 0 || cv.RecruitmentPost.MajorId == param.MajorId)
                                                                                && cv.CV.EmployeeId == id);
            Func<IQueryable<Application>, IOrderedQueryable<Application>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = cv => cv.OrderBy(cv => cv.RecruitmentPost.Created);
                    break;
                case "+salary":
                    order = cv => cv.OrderBy(cv => cv.RecruitmentPost.MaxSalary);
                    break;
                case "-salary":
                    order = cv => cv.OrderByDescending(cv => cv.RecruitmentPost.MaxSalary);
                    break;
                default:
                    order = cv => cv.OrderByDescending(cv => cv.RecruitmentPost.Created);
                    break;
            }
            IEnumerable<Application> appliedCVs = await _uow.AppliedCVRepository.Get(filter: filter,
                                                                                    orderBy: order,
                                                                                    includeProperties: "RecruitmentPost,RecruitmentPost.Company,RecruitmentPost.Major,RecruitmentPost.Company.Account,CV",
                                                                                    first: param.Limit,
                                                                                    offset: param.Offset);
            IEnumerable<RecruitmentPost> listAppliedJobs = appliedCVs.Select(cv => cv.RecruitmentPost);
            return _mapper.Map<List<CommonRecruitmentPostDataset>>(listAppliedJobs);
        }

        public async Task<List<CVDataset>> GetListCVs(int id, CVRequestParam param)
        {
            Expression<Func<Models.CV, bool>> filter = cv => cv.EmployeeId == id &&
                                                            (param.MajorId == 0 || cv.MajorId == param.MajorId) &&
                                                            cv.Title.Contains(param.Title);
            Func<IQueryable<Models.CV>, IOrderedQueryable<Models.CV>> order;
            switch (param.OrderBy)
            {
                case "+created":
                    order = cv => cv.OrderBy(cv => cv.Created);
                    break;
                default:
                    order = cv => cv.OrderByDescending(cv => cv.Created);
                    break;
            }
            IEnumerable<Models.CV> list = await _uow.CVRepository.Get(filter: filter,
                                                                orderBy: order,
                                                                includeProperties: "Employee,Employee.Account",
                                                                first: param.Limit,
                                                                offset: param.Offset);
            return _mapper.Map<List<CVDataset>>(list);
        }

        public async Task<EmployeeDataset> UpdateInfo(EmployeeInfoParam param)
        {
            Models.Employee employee = await _uow.EmployeeRepository.GetById(param.AccountId);
            employee.FullName = param.FullName;
            employee.Address = param.Address;
            employee.DateOfBirth = param.DateOfBirth;
            employee.Gender = param.Gender;
            employee.Phone = param.Phone;
            _uow.EmployeeRepository.Update(employee);
            if (await _uow.CommitAsync() > 0)
            {
                return _mapper.Map<EmployeeDataset>(await _uow.EmployeeRepository.GetFirst(filter: emp => emp.AccountId == param.AccountId, includeProperties: "Account"));
            }
            return null;
        }
    }
}
