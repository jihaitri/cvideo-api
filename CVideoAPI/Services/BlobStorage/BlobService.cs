using CVideoAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CVideoAPI.Services.BlobStorage
{
    public class BlobService : IBlobService
    {
        private readonly string accessKey;
        public BlobService()
        {
            accessKey = AppConfig.GetConnectionString("CVideoStorage");
        }
        private string GenerateFileName(string fileName, string nameKey)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = nameKey + "-" + DateTime.UtcNow.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
        private CloudBlobContainer AccessToBlob()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            string strContainerName = "uploads";
            return cloudBlobClient.GetContainerReference(strContainerName);
        }
        public async Task<string> UploadFileToBlobAsync(IFormFile file, string nameKey)
        {
            CloudBlobContainer cloudBlobContainer = AccessToBlob();
            string fileName = this.GenerateFileName(file.FileName, nameKey);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            if (file != null)
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            return "";
        }
        public async Task DeleteFile(string path)
        {
            CloudBlobContainer cloudBlobContainer = AccessToBlob();
            string blobName = Path.GetFileName(new Uri(path).LocalPath);
            string pathPrefix = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/";
            CloudBlobDirectory blobDirectory = cloudBlobContainer.GetDirectoryReference(pathPrefix);
            // get block blob refarence    
            CloudBlockBlob blockBlob = blobDirectory.GetBlockBlobReference(blobName);
            await blockBlob.DeleteAsync();
        }
    }
}
