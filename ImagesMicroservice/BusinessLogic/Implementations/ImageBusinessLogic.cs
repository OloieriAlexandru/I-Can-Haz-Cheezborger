using BusinessLogic.Abstractions;
using Common.Utils;
using DataAccess.Abstractions;
using Entities;
using Microsoft.Extensions.Options;
using Models.Images;
using System;
using System.Text;

namespace BusinessLogic.Implementations
{
    public class ImageBusinessLogic : IImageBusinessLogic
    {
        private readonly IImageInfoRepository imageInfoRepository;

        private readonly IFileRepository fileRepository;

        private readonly GoogleCloudConfig googleCloudConfig;

        public ImageBusinessLogic(IImageInfoRepository imageInfoRepository, IFileRepository fileRepository,
            IOptionsMonitor<GoogleCloudConfig> optionsMonitor)
        {
            this.imageInfoRepository = imageInfoRepository;
            this.fileRepository = fileRepository;
            this.googleCloudConfig = optionsMonitor.CurrentValue;
        }

        ImageGetDto IImageBusinessLogic.Create(ImageCreateDto imageCreateDto)
        {
            string[] imageSplitParts = imageCreateDto.Image.Split(';', 2);
            if (imageSplitParts.Length < 2)
            {
                return null;
            }
            string imageType = imageSplitParts[0].Split(':')[1];
            byte[] imageBytes = Convert.FromBase64String(imageSplitParts[1].Split(',', 2)[1]);

            Guid imageId = Guid.NewGuid();
            string imageUrl = imageCreateDto.Prefix + "/" + imageId.ToString();

            fileRepository.Create(imageUrl, imageBytes);

            ImageInfo imageInfo = new ImageInfo()
            {
                FilePath = imageUrl,
                ImageType = imageType
            };
            imageInfoRepository.Create(imageInfo);

            return new ImageGetDto()
            {
                Id = imageInfo.Id,
                Url = $"api/v1/images/{imageInfo.Id}/image"
            };
        }

        ImageGetDto IImageBusinessLogic.Get(string id)
        {
            ImageInfo imageInfo = imageInfoRepository.Get(id);
            if (imageInfo == null)
            {
                return null;
            }
            return new ImageGetDto()
            {
                Id = id,
                Url = imageInfo.FilePath
            };
        }

        ImageFileGetDto IImageBusinessLogic.GetImage(string id)
        {
            ImageInfo imageInfo = imageInfoRepository.Get(id);
            if (imageInfo == null)
            {
                return null;
            }
            byte[] imageBytes = fileRepository.Get(imageInfo.FilePath);
            if (imageBytes == null)
            {
                return null;
            }
            return new ImageFileGetDto()
            {
                Image = imageBytes,
                ImageType = imageInfo.ImageType
            };
        }

        void IImageBusinessLogic.Update(ImageUpdateDto imageUpdateDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
