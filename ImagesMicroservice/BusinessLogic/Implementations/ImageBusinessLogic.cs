using BusinessLogic.Abstractions;
using BusinessLogic.Utils;
using Common.Utils;
using DataAccess.Abstractions;
using Entities;
using Microsoft.Extensions.Options;
using Models.Images;
using System;

namespace BusinessLogic.Implementations
{
    public class ImageBusinessLogic : IImageBusinessLogic
    {
        private readonly IImageInfoRepository imageInfoRepository;

        private readonly IFileRepository fileRepository;

        public ImageBusinessLogic(IImageInfoRepository imageInfoRepository, IFileRepository fileRepository)
        {
            this.imageInfoRepository = imageInfoRepository;
            this.fileRepository = fileRepository;
        }

        ImageGetDto IImageBusinessLogic.Create(ImageCreateDto imageCreateDto)
        {
            string imageType = string.Empty;
            byte[] imageBytes = Array.Empty<byte>();
            Base64Extractor.Extract(imageCreateDto.Image, ref imageType, ref imageBytes);

            string imageUrl = $"{imageCreateDto.Prefix}/{Guid.NewGuid().ToString()}";

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
