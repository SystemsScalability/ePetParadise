using api.DTOs.Entities;
using api.Models.Transactions;
using api.Services;
using api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _service;

        public SalesController(SaleService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetSales")]
    public async Task<IEnumerable<Sale>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}", Name = "GetSale")]
    public async Task<ActionResult<Sale>> GetById(int id)
    {
        var sale = await _service.GetByID(id);

        if ( id <= 0 )
        {
          return ErrorUtilities.IdPositive(id);
        }

        if (sale == null)
        {
          return ErrorUtilities.FieldNotFound("Sale", id);
        }
        return sale;
    }

    [HttpPost(Name = "AddSale")]
    public async Task<IActionResult> Create(SaleDTO saleDTO)
    {
        var newSale = await _service.Create(saleDTO);

        return CreatedAtAction(nameof(GetById), new { id = newSale.SaleID }, saleDTO);
    }

    [HttpPut("{id}", Name = "EditSale")]
    public async Task<IActionResult> Update(int id, SaleDTO saleDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var saleToUpdate = await _service.GetByID(id);

      if (saleToUpdate is not null)
      {
        await _service.Update(id, saleDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Sale", id);
      }
    }
  }
}
