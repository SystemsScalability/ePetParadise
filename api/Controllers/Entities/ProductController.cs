using api.Services;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Utilities;
using api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<Product>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _service.GetByID(id);

        if (product == null)
        {
            return ErrorUtilities.FieldNotFound("Product", id);
        }
        return product;
    }

    [HttpPost(Name = "AddProduct")]
    [Authorize]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        var newProduct = await _service.Create(productDTO);
        if (newProduct.Name.Equals("error_409_validations")) {
           return ErrorUtilities.UniqueName("Product");
        }

        return CreatedAtAction(nameof(GetById), new { id = newProduct.ProductID }, productDTO);
    }

    [HttpPut("{id}", Name = "EditProduct")]
    [Authorize]
    public async Task<IActionResult> Update(int id, ProductDTO productDTO)
    {
      if ( id <= 0 )
      {
          return ErrorUtilities.IdPositive(id);
      }

      var productToUpdate = await _service.GetByID(id);

      if (productToUpdate is not null)
      {
        await _service.Update(id, productDTO);
        return NoContent();
      }
      else
      {
        return ErrorUtilities.FieldNotFound("Product", id);
      }
    }
  }
}
