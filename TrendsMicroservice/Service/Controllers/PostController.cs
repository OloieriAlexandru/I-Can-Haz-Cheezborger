using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Common;
using Models.Posts;
using Service.Utils;
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

        [HttpGet("auth")]
        public ICollection<PostGetAllDto> GetAllAuthorized([FromRoute] Guid trendId)
        {
            UserInfoModel userInfoModel = new UserInfoModel();
            UserInfoExtractor.Extract(HttpContext.User, userInfoModel);

            return postBusinessLogic.GetAll(trendId, userInfoModel);
        }

        [HttpGet]
        public ICollection<PostGetAllDto> GetAllUnauthorized([FromRoute] Guid trendId)
        {
            return postBusinessLogic.GetAll(trendId, null);
        }

        [HttpGet("~/api/v1/trends/posts")]
        public ICollection<PostGetAllDto> GetRecommended()
        {
            return new List<PostGetAllDto>();
        }

        [HttpGet("{id:guid}/auth")]
        [Authorize]
        public ActionResult<PostGetByIdDto> GetByIdAuthorized([FromRoute] Guid id)
        {
            UserInfoModel userInfoModel = new UserInfoModel();
            UserInfoExtractor.Extract(HttpContext.User, userInfoModel);

            PostGetByIdDto postDto = postBusinessLogic.GetById(id, userInfoModel);
            if (postDto == null)
            {
                return NotFound();
            }
            return Ok(postDto);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<PostGetByIdDto> GetByIdUnauthorized([FromRoute] Guid id)
        {
            PostGetByIdDto postDto = postBusinessLogic.GetById(id, null);
            if (postDto == null)
            {
                return NotFound();
            }
            return Ok(postDto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromRoute] Guid trendId, [FromBody] PostCreateDto postDto)
        {
            if (trendId != postDto.TrendId)
            {
                return BadRequest();
            }
            UserInfoExtractor.Extract(HttpContext.User, postDto);

            PostGetAllDto createdPost = postBusinessLogic.Create(postDto);
            return CreatedAtAction(nameof(GetByIdUnauthorized), new { trendId, id = createdPost.Id }, createdPost);
        }
        
        [HttpPatch("{id:guid}")]
        public IActionResult Patch([FromRoute] Guid id, [FromBody] PostPatchDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest();
            }
            postBusinessLogic.Patch(postDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/react")]
        public IActionResult PatchReact([FromRoute] Guid id, [FromBody] PostPatchReactDto postPatchReactDto)
        {
            if (id != postPatchReactDto.Id)
            {
                return BadRequest();
            }
            UserInfoExtractor.Extract(HttpContext.User, postPatchReactDto);
            postBusinessLogic.PatchReact(postPatchReactDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/content-scan-result")]
        public IActionResult PatchContentScanTaskApprovals([FromRoute] Guid id, [FromBody] PatchContentScanTaskApprovalsDto contentScanTaskApprovalsDto)
        {
            if (id != contentScanTaskApprovalsDto.ObjectId)
            {
                return BadRequest();
            }
            postBusinessLogic.PatchContentScanTaskApprovals(id, contentScanTaskApprovalsDto);
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
