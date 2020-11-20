using AutoMapper;
using CVideoAPI.Datasets.Admin;
using CVideoAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AdminService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<RegisterInDayDataset>> GetDailyRegister(DateTime from, DateTime to)
        {
            IEnumerable<Models.Account> accounts = await _uow.AccountRepository.Get(filter: acc => acc.Created >= from && acc.Created <= to,
                                                                                    orderBy: acc => acc.OrderBy(acc => acc.Created));
            List<RegisterInDayDataset> result = accounts.GroupBy(acc => new
            {
                Month = acc.Created.Month,
                Day = acc.Created.Day,
                Year = acc.Created.Year,
                FullDate = acc.Created
            }).Select(acc => new RegisterInDayDataset
            {
                Date = new DateTime(acc.Key.Year, acc.Key.Month, acc.Key.Day).ToString("yyyyMMdd"),
                NumOfRegister = acc.Count()
            }).ToList();
            return result;
        }
        public async Task<List<CreatedCVsInDayDataset>> GetDailyCreatedCVs(DateTime from, DateTime to)
        {
            IEnumerable<Models.CV> cvs = await _uow.CVRepository.Get(filter: cv => cv.Created >= from && cv.Created <= to,
                                                                                    orderBy: cv => cv.OrderBy(cv => cv.Created));
            List<CreatedCVsInDayDataset> result = cvs.GroupBy(cv => new
            {
                Month = cv.Created.Month,
                Day = cv.Created.Day,
                Year = cv.Created.Year,
                FullDate = cv.Created
            }).Select(cv => new CreatedCVsInDayDataset
            {
                Date = new DateTime(cv.Key.Year, cv.Key.Month, cv.Key.Day).ToString("yyyyMMdd"),
                NumOfCreatedCVs = cv.Count()
            }).ToList();
            return result;
        }
    }
}
