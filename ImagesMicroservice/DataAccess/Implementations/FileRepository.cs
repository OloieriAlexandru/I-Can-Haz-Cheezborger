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

        private readonly GoogleCloudConfig googleCloudConfig;

        public FileRepository(StorageClient storageClient, IOptionsMonitor<GoogleCloudConfig> optionsMonitor)
        {
            this.storageClient = storageClient;
            this.googleCloudConfig = optionsMonitor.CurrentValue;
            this.imagesBucket = this.storageClient.GetBucket(this.googleCloudConfig.BucketName);
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
