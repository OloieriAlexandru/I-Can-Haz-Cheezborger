using Models.Images;

namespace BusinessLogic.Abstractions
{
    public interface IImageBusinessLogic
    {
        ImageGetDto Get(string id);

        ImageFileGetDto GetImage(string id);

        ImageGetDto Create(ImageCreateDto imageCreateDto);

        void Update(ImageUpdateDto imageUpdateDto);
    }
}
