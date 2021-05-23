using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Images;
using System.IO;

namespace Service.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageBusinessLogic imageBusinessLogic;

        public ImageController(IImageBusinessLogic imageBusinessLogic)
        {
            this.imageBusinessLogic = imageBusinessLogic;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            ImageGetDto imageGetDto = imageBusinessLogic.Get(id);
            if (imageGetDto == null)
            {
                return NotFound();
            }
            return Ok(imageGetDto);
        }

        [HttpGet("{id}/image")]
        public IActionResult GetImage([FromRoute] string id)
        {
            ImageFileGetDto image = imageBusinessLogic.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image.Image, image.ImageType);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ImageCreateDto imageCreateDto)
        {
            ImageGetDto imageGetDto = imageBusinessLogic.Create(imageCreateDto);
            if (imageGetDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = imageGetDto.Id }, imageGetDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] string id, ImageUpdateDto imageUpdateDto)
        {
            if (id != imageUpdateDto.Id)
            {
                return BadRequest();
            }
            imageBusinessLogic.Update(imageUpdateDto);
            return NoContent();
        }
    }
}
