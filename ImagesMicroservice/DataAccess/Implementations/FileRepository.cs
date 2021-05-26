using Common.Utils;
using DataAccess.Abstractions;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using System.IO;

namespace DataAccess.Implementations
{
    public class FileRepository : IFileRepository
    {
        private readonly StorageClient storageClient;

        private readonly Bucket imagesBucket;

        public FileRepository(StorageClient storageClient, IOptionsMonitor<GoogleCloudConfig> optionsMonitor)
        {
            this.storageClient = storageClient;
        
            imagesBucket = this.storageClient.GetBucket(optionsMonitor.CurrentValue.BucketName);
        }

        void IFileRepository.Create(string fileName, byte[] fileContent)
        {
            this.storageClient.UploadObject(this.imagesBucket.Name, fileName, null, new MemoryStream(fileContent));
        }

        byte[] IFileRepository.Get(string fileName)
        {
            MemoryStream memoryStream = new MemoryStream();
            this.storageClient.DownloadObject(this.imagesBucket.Name, fileName, memoryStream);
            return memoryStream.ToArray();
        }
    }
}
