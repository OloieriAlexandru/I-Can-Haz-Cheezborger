using Models.ImagesS;

namespace BusinessLogic.Abstractions
{
    public interface IImageService
    {
        ImageGetDto Create(ImageCreateDto imageCreateDto);

        string GetDefaultImageUrl();
    }
}
