using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace Service.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBusinessLogic postBusinessLogic;

        public PostController(IPostBusinessLogic _postBusinessLogic)
        {
            postBusinessLogic = _postBusinessLogic;
        }

        [HttpGet]
        public ICollection<PostDto> GetAll()
        {
            return postBusinessLogic.GetAll();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(PostDto))]
        [ProducesResponseType(404)]
        public ActionResult<PostDto> GetById([FromRoute] Guid id)
        {
            PostDto postDto = postBusinessLogic.GetById(id);
            if (postDto == null)
            {
                return NotFound();
            }
            return Ok(postDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostDto postDto)
        {
            postBusinessLogic.Create(postDto);
            return CreatedAtAction(nameof(GetById), new { id = postDto.Id }, postDto);

        }
        
        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] PostDto postDto)
        {
            if (!postDto.Id.HasValue || id != postDto.Id.Value)
            {
                return BadRequest();
            }
            postBusinessLogic.Update(postDto);
            return NoContent();
        }
    }
}
