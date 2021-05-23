using Models.Images;

namespace BusinessLogic.Abstractions
{
    public interface IImageService
    {
        ImageGetDto Create(ImageCreateDto imageCreateDto);

        string GetFullImageUrl(string imagePath);
    }
}
