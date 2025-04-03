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
    public class BrandController : ControllerBase
    {
        private readonly BrandService _service;

        public BrandController(BrandService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetBrands")]
    public async Task<IEnumerable<Brand>> Get()
    {
        return await _service.GetAll();
    } 


    [HttpGet("{id}", Name = "GetBrand")]
    public async Task<ActionResult<Brand>> GetById(int id)
    {
        var brand = await _service.GetByID(id);

        if ( id <= 0 )
        {
          return ErrorUtilities.IdPositive(id);
        }

        if (brand == null)
        {
          return ErrorUtilities.FieldNotFound("Brand", id);
        }
        return brand;
    }

    [HttpPost(Name = "AddBrand")]
    [Authorize]
    public async Task<IActionResult> Create(BrandDTO brandDTO)
    {
        var newBrand = await _service.Create(brandDTO);
        if (newBrand.Name.Equals("error_409_validations")) {
           return ErrorUtilities.UniqueName("Brand");
        }

        return CreatedAtAction(nameof(GetById), new { id = newBrand.BrandID }, brandDTO);
    }

    [HttpPut("{id}", Name = "EditBrand")]
    [Authorize]
    public async Task<IActionResult> Update(int id, BrandDTO brandDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var brandToUpdate = await _service.GetByID(id);

      if (brandToUpdate is not null)
      {
        await _service.Update(id, brandDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Brand", id);
      }
    }
    }
}
