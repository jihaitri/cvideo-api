using CVideoAPI.Datasets.Major;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Major
{
    public interface IMajorService
    {
        Task<List<MajorDataset>> GetMajors(string lang);
    }
}
