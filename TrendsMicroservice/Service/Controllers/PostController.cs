using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace Service.Controllers
{
    [Route("api/v1/trends/{trendId:guid}/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBusinessLogic postBusinessLogic;

        public PostController(IPostBusinessLogic _postBusinessLogic)
        {
            postBusinessLogic = _postBusinessLogic;
        }

        [HttpGet]
        public ICollection<PostGetAllDto> GetAll([FromRoute] Guid trendId)
        {
            return postBusinessLogic.GetAll(trendId);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<PostGetByIdDto> GetById([FromRoute] Guid id)
        {
            PostGetByIdDto postDto = postBusinessLogic.GetById(id);
            if (postDto == null)
            {
                return NotFound();
            }
            return Ok(postDto);
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid trendId, [FromBody] PostCreateDto postDto)
        {
            if (trendId != postDto.TrendId)
            {
                return BadRequest();
            }
            PostGetAllDto createdPost = postBusinessLogic.Create(postDto);
            return CreatedAtAction(nameof(GetById), new { trendId = trendId, id = createdPost.Id }, createdPost);
        }
        
        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid trendId, [FromRoute] Guid id, [FromBody] PostUpdateDto postDto)
        {
            if (id != postDto.Id || trendId != postDto.TrendId)
            {
                return BadRequest();
            }
            postBusinessLogic.Update(postDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            postBusinessLogic.Delete(id);
            return NoContent();
        }
    }
}
