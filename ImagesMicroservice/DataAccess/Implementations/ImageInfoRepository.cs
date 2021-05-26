using Common.Utils;
using DataAccess.Abstractions;
using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Implementations
{
    public class ImageInfoRepository : IImageInfoRepository
    {
        private readonly IMongoCollection<ImageInfo> images;

        public ImageInfoRepository(MongoClient mongoClient, IOptionsMonitor<MongoDbConfig> optionsMonitor)
        {
            images = mongoClient.GetDatabase(optionsMonitor.CurrentValue.DatabaseName)
                .GetCollection<ImageInfo>(optionsMonitor.CurrentValue.CollectionName);
        }

        void IImageInfoRepository.Create(ImageInfo imageInfo)
        {
            images.InsertOne(imageInfo);
        }

        ImageInfo IImageInfoRepository.Get(string id)
        {
            return images.Find<ImageInfo>(i => i.Id == id).FirstOrDefault();
        }

        void IImageInfoRepository.Update(string id, ImageInfo imageInfo)
        {
            images.ReplaceOne(i => i.Id == id, imageInfo);
        }
    }
}
