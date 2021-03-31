using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
// https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0
namespace Service.Controllers
{
    [Route("api/v1/trends")]
    [ApiController]
    public class TrendController : ControllerBase
    {
        private readonly ITrendBusinessLogic trendBusinessLogic;

        public TrendController(ITrendBusinessLogic _trendBusinessLogic)
        {
            trendBusinessLogic = _trendBusinessLogic;
        }

        [HttpGet]
        public ICollection<TrendDto> GetAll()
        {
            return trendBusinessLogic.GetAll();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(TrendDto))]
        [ProducesResponseType(404)]
        public ActionResult<TrendDto> GetById([FromRoute] Guid id)
        {
            TrendDto trendDto = trendBusinessLogic.GetById(id);
            if (trendDto == null)
            {
                return NotFound();
            }
            return Ok(trendDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TrendDto trendDto)
        {
            trendBusinessLogic.Create(trendDto);
            return CreatedAtAction(nameof(GetById), new { id = trendDto.Id }, trendDto);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] TrendDto trendDto)
        {
            if (!trendDto.Id.HasValue || id != trendDto.Id.Value)
            {
                return BadRequest();
            }
            trendBusinessLogic.Update(trendDto);
            return NoContent();
        }
    }
}
