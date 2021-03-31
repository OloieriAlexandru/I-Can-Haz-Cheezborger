using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace Service.Controllers
{
    [Route("api/v1/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentBusinessLogic commentBusinessLogic;

        public CommentController(ICommentBusinessLogic _commentBusinessLogic)
        {
            commentBusinessLogic = _commentBusinessLogic;
        }

        [HttpGet]
        public ICollection<CommentDto> GetAll()
        {
            return commentBusinessLogic.GetAll();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(CommentDto))]
        [ProducesResponseType(404)]
        public ActionResult<CommentDto> GetById([FromRoute] Guid id)
        {
            CommentDto commentDto = commentBusinessLogic.GetById(id);
            if (commentDto == null)
            {
                return NotFound();
            }
            return Ok(commentDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CommentDto commentDto)
        {
            commentBusinessLogic.Create(commentDto);
            return CreatedAtAction(nameof(GetById), new { id = commentDto.Id }, commentDto);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CommentDto commentDto)
        {
            if (!commentDto.Id.HasValue || id != commentDto.Id.Value)
            {
                return BadRequest();
            }
            commentBusinessLogic.Update(commentDto);
            return NoContent();
        }
    }
}
