using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
      return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByID(int id)
    {
      return await _context.Products.FindAsync(id);
    }

    public async Task<Product> Create(ProductDTO newProductDTO)
    {
      if (await IsBrandNameUnique(newProductDTO.Name))
      {
        var product = new Product();
        product.Name = "error_409_validations";
        return product;
      }

      var newProduct = new Product();
      newProduct.ProductID = await GetCount() + 1;
      newProduct.Name = newProductDTO.Name;
      newProduct.Price = newProductDTO.Price;
      newProduct.Quantity = newProductDTO.Quantity;
      newProduct.Discount = newProductDTO.Discount;
      newProduct.AnimalCategory = newProductDTO.AnimalCategory;
      newProduct.Image = newProductDTO.Image;
      newProduct.Description = newProductDTO.Description;
      newProduct.ProductType = newProductDTO.ProductType;
      newProduct.BrandID = newProductDTO.BrandID;
      newProduct.ProviderID = newProductDTO.ProviderID;

      if (newProductDTO.IsAvailable) {
        newProduct.IsAvailable = "true";
      } else {
        newProduct.IsAvailable = "false";
      }

      if (newProductDTO.HasTax) {
        newProduct.HasTax = "true";
      } else {
        newProduct.HasTax = "false";
      }

      _context.Products.Add(newProduct);
      await _context.SaveChangesAsync();

      return newProduct;
    }

    public async Task Update(int id, ProductDTO productDTO)
    {
      var existingProduct = await GetByID(id);

      if (existingProduct is not null)
      {
      existingProduct.Name = productDTO.Name;
      existingProduct.Price = productDTO.Price;
      existingProduct.Quantity = productDTO.Quantity;
      existingProduct.Discount = productDTO.Discount;
      existingProduct.AnimalCategory = productDTO.AnimalCategory;
      existingProduct.Image = productDTO.Image;
      existingProduct.Description = productDTO.Description;
      existingProduct.BrandID = productDTO.BrandID;
      existingProduct.ProviderID = productDTO.ProviderID;

      if (productDTO.IsAvailable) {
        existingProduct.IsAvailable = "true";
      } else {
        existingProduct.IsAvailable = "false";
      }

      if (productDTO.HasTax) {
        existingProduct.HasTax = "true";
      } else {
        existingProduct.HasTax = "false";
      }

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var productToDelete = await GetByID(id);

      if(productToDelete is not null)
      {
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Products.CountAsync();
    }

    public async Task<bool> IsBrandNameUnique(string productName)
    {
    var products = await _context.Products.AsNoTracking().ToListAsync();
    return products.Any(b => string.Equals(b.Name, productName, StringComparison.OrdinalIgnoreCase));
    }
    }
}
