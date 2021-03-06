using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Common;
using Models.Trends;
using Service.Utils;
using System;
using System.Collections.Generic;

namespace Service.Controllers
{
    [Route("api/v1/trends")]
    [ApiController]
    public class TrendController : Controller
    {
        private readonly ITrendBusinessLogic trendBusinessLogic;

        public TrendController(ITrendBusinessLogic _trendBusinessLogic)
        {
            trendBusinessLogic = _trendBusinessLogic;
        }

        [HttpGet("auth")]
        [Authorize]
        public ICollection<TrendGetAllDto> GetAllAuthorized()
        {
            UserInfoModel userInfoModel = new UserInfoModel();
            UserInfoExtractor.Extract(HttpContext.User, userInfoModel);

            return trendBusinessLogic.GetAll(userInfoModel);
        }

        [HttpGet]
        public ICollection<TrendGetAllDto> GetAll()
        {
            return trendBusinessLogic.GetAll(null);
        }

        [HttpGet("popular")]
        public ICollection<TrendGetAllDto> GetPopular()
        {
            return trendBusinessLogic.GetPopular();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(TrendGetByIdDto))]
        [ProducesResponseType(404)]
        public ActionResult<TrendGetByIdDto> GetById([FromRoute] Guid id)
        {
            TrendGetByIdDto trend = trendBusinessLogic.GetById(id);
            if (trend == null)
            {
                return NotFound();
            }
            return Ok(trend);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] TrendCreateDto trendDto)
        {
            UserInfoExtractor.Extract(HttpContext.User, trendDto);
            TrendGetAllDto createdTrend = trendBusinessLogic.Create(trendDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTrend.Id }, createdTrend);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] TrendUpdateDto trendDto)
        {
            if (id != trendDto.Id)
            {
                return BadRequest();
            }
            trendBusinessLogic.Update(trendDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/follow")]
        [Authorize]
        public IActionResult PatchFollow([FromRoute] Guid id, [FromBody] TrendPatchFollowDto trendPatchFollowDto)
        {
            if (id != trendPatchFollowDto.Id)
            {
                return BadRequest();
            }
            UserInfoExtractor.Extract(HttpContext.User, trendPatchFollowDto);
            trendBusinessLogic.PatchFollow(trendPatchFollowDto);
            return NoContent();
        }

        [HttpPatch("{id:guid}/content-scan-result")]
        public IActionResult PatchContentScanTaskApprovals([FromRoute] Guid id, [FromBody] PatchContentScanTaskApprovalsDto contentScanTaskApprovalsDto)
        {
            if (id != contentScanTaskApprovalsDto.ObjectId)
            {
                return BadRequest();
            }
            trendBusinessLogic.PatchContentScanTaskApprovals(id, contentScanTaskApprovalsDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            trendBusinessLogic.Delete(id);
            return NoContent();
        }
    }
}
