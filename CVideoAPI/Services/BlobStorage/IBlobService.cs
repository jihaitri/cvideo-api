using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CVideoAPI.Services.BlobStorage
{
    public interface IBlobService
    {
        Task<string> UploadFileToBlobAsync(IFormFile file, string nameKey);
        Task DeleteFile(string path);
    }
}
