using Entities;

namespace DataAccess.Abstractions
{
    public interface IImageInfoRepository
    {
        ImageInfo Get(string id);

        void Create(ImageInfo imageInfo);

        void Update(string id, ImageInfo imageInfo);
    }
}
