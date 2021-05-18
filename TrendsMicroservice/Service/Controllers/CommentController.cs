using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Comments;
using Models.Common;
using Service.Utils;
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
        [Authorize]
        public IActionResult Create([FromRoute] Guid trendId, [FromRoute] Guid postId, [FromBody] CommentCreateDto commentDto)
        {
            UserInfoExtractor.Extract(HttpContext.User, commentDto);

            if (postId != commentDto.PostId)
            {
                return BadRequest();
            }
            CommentGetDto createdComment = commentBusinessLogic.Create(commentDto);
            return CreatedAtAction(nameof(GetById), new { trendId, postId, id = createdComment.Id }, createdComment);
        }

        [HttpPatch("{id:guid}")]
        public IActionResult Patch([FromRoute] Guid id, [FromBody] CommentPatchDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }
            commentBusinessLogic.Patch(commentDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/react")]
        public IActionResult PatchReact([FromRoute] Guid id, [FromBody] CommentPatchReactDto commentPatchReactDto)
        {
            if (id != commentPatchReactDto.Id)
            {
                return BadRequest();
            }
            UserInfoExtractor.Extract(HttpContext.User, commentPatchReactDto);
            commentBusinessLogic.PatchReact(commentPatchReactDto);
            return NoContent();
        }
        
        [HttpPatch("{id:guid}/content-scan-result")]
        public IActionResult PatchContentScanTaskApprovals([FromRoute] Guid id, [FromBody] PatchContentScanTaskApprovalsDto contentScanTaskApprovalsDto)
        {
            if (id != contentScanTaskApprovalsDto.ObjectId)
            {
                return BadRequest();
            }
            commentBusinessLogic.PatchContentScanTaskApprovals(id, contentScanTaskApprovalsDto);
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
