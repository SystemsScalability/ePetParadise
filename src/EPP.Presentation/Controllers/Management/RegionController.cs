using api.DTOs;
using api.Models;
using api.Services;
using api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly RegionService _service;

        public RegionController(RegionService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetRegions")]
    public async Task<IEnumerable<Region>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}", Name = "GetRegion")]
    public async Task<ActionResult<Region>> GetById(int id)
    {
        var region = await _service.GetByID(id);

        if ( id <= 0 )
        {
          return ErrorUtilities.IdPositive(id);
        }

        if (region == null)
        {
          return ErrorUtilities.FieldNotFound("Region", id);
        }
        return region;
    }

    [HttpPost(Name = "AddRegion")]
    [Authorize]
    public async Task<IActionResult> Create(RegionDTO regionDTO)
    {
        var newRegion = await _service.Create(regionDTO);

        return CreatedAtAction(nameof(GetById), new { id = newRegion.RegionID }, regionDTO);
    }

    [HttpPut("{id}", Name = "EditRegion")]
    [Authorize]
    public async Task<IActionResult> Update(int id, RegionDTO regionDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var regionToUpdate = await _service.GetByID(id);

      if (regionToUpdate is not null)
      {
        await _service.Update(id, regionDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Region", id);
      }
    } 
  }
}
