using CVideoAPI.Datasets.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Admin
{
    public interface IAdminService
    {
        Task<List<RegisterInDayDataset>> GetDailyRegister(DateTime from, DateTime to);
        Task<List<CreatedCVsInDayDataset>> GetDailyCreatedCVs(DateTime from, DateTime to);
    }
}
