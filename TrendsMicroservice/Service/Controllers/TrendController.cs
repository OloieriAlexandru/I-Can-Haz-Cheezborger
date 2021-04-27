using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Trends;
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

        [HttpGet]
        public ICollection<TrendGetAllDto> GetAll()
        {
            return trendBusinessLogic.GetAll();
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
        public IActionResult Create([FromBody] TrendCreateDto trendDto)
        {
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

        [HttpDelete("{id:guid}")]
        [Authorize]
        public IActionResult Delete([FromRoute] Guid id)
        {
            trendBusinessLogic.Delete(id);
            return NoContent();
        }
    }
}
