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
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService _service;
        
        public ProviderController(ProviderService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetProviders")]
    public async Task<IEnumerable<Provider>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}", Name = "GetProvider")]
    public async Task<ActionResult<Provider>> GetById(int id)
    {
        var provider = await _service.GetByID(id);

        if ( id <= 0 )
        {
          return ErrorUtilities.IdPositive(id);
        }

        if (provider == null)
        {
          return ErrorUtilities.FieldNotFound("Provider", id);
        }
        return provider;
    }

    [HttpPost(Name = "AddProvider")]
    [Authorize]
    public async Task<IActionResult> Create(ProviderDTO providerDTO)
    {
        var newProvider = await _service.Create(providerDTO);
        if (newProvider.Name.Equals("error_409_validations")) {
           return ErrorUtilities.UniqueName("Provider");
        }

        return CreatedAtAction(nameof(GetById), new { id = newProvider.ProviderID }, providerDTO);
    }

    [HttpPut("{id}", Name = "EditProvider")]
    [Authorize]
    public async Task<IActionResult> Update(int id, ProviderDTO providerDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var providerToUpdate = await _service.GetByID(id);

      if (providerToUpdate is not null)
      {
        await _service.Update(id, providerDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Provider", id);
      }
    }
  }
}
