﻿using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace Service.Controllers
{
    [Route("api/v1/trends/{trendId:guid}/posts/{postId:guid}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentBusinessLogic commentBusinessLogic;

        public CommentController(ICommentBusinessLogic _commentBusinessLogic)
        {
            commentBusinessLogic = _commentBusinessLogic;
        }

        [HttpGet]
        public ICollection<CommentGetDto> GetAll([FromRoute] Guid postId)
        {
            return commentBusinessLogic.GetAll(postId);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(CommentGetDto))]
        [ProducesResponseType(404)]
        public ActionResult<CommentGetDto> GetById([FromRoute] Guid id)
        {
            CommentGetDto commentDto = commentBusinessLogic.GetById(id);
            if (commentDto == null)
            {
                return NotFound();
            }
            return Ok(commentDto);
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid trendId, [FromRoute] Guid postId, [FromBody] CommentCreateDto commentDto)
        {
            if (postId != commentDto.PostId)
            {
                return BadRequest();
            }
            CommentGetDto createdComment = commentBusinessLogic.Create(commentDto);
            return CreatedAtAction(nameof(GetById), new { trendId = trendId, postId = postId, id = createdComment.Id }, createdComment);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid postId, [FromRoute] Guid id, [FromBody] CommentUpdateDto commentDto)
        {
            if (id != commentDto.Id || postId != commentDto.PostId)
            {
                return BadRequest();
            }
            commentBusinessLogic.Update(commentDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            commentBusinessLogic.Delete(id);
            return NoContent();
        }
    }
}
