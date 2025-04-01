using api.DTOs;
using api.Models;
using api.Services;
using api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _service;

        public PurchaseController(PurchaseService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetPurchases")]
    public async Task<IEnumerable<Purchase>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}", Name = "GetPurchase")]
    public async Task<ActionResult<Purchase>> GetById(int id)
    {
        var purchase = await _service.GetByID(id);

        if ( id <= 0 )
        {
          return ErrorUtilities.IdPositive(id);
        }

        if (purchase == null)
        {
          return ErrorUtilities.FieldNotFound("Purchase", id);
        }
        return purchase;
    }

    [HttpPost(Name = "AddPurchase")]
    public async Task<IActionResult> Create(PurchaseDTO purchaseDTO)
    {
        var newPurchase = await _service.Create(purchaseDTO);

        return CreatedAtAction(nameof(GetById), new { id = newPurchase.PurchaseID }, purchaseDTO);
    }

    [HttpPut("{id}", Name = "EditPurchase")]
    public async Task<IActionResult> Update(int id, PurchaseDTO purchaseDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var purchaseToUpdate = await _service.GetByID(id);

      if (purchaseToUpdate is not null)
      {
        await _service.Update(id, purchaseDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Purchase", id);
      }
    }
  }
}
