using api.Data;
using api.DTOs;
using api.Models;
using api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class BrandService
    {
        private readonly DataContext _context;

        public BrandService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brand>> GetAll()
    {
      return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetByID(int id)
    {
      return await _context.Brands.FindAsync(id);
    }

    public async Task<Brand> Create([FromBody] BrandDTO newBrandDTO)
    {
      if (await IsBrandNameUnique(newBrandDTO.Name))
      {
        var brand = new Brand();
        brand.Name = "error_409_validations";
        return brand;
      }

      var newBrand = new Brand();
      newBrand.BrandID = await GetCount() + 1;
      newBrand.Name = newBrandDTO.Name;
      newBrand.Logo = newBrandDTO.Logo;
      if (newBrandDTO.IsAvailable) {
        newBrand.IsAvailable = "true";
      } else {
        newBrand.IsAvailable = "false";
      }
      
      _context.Brands.Add(newBrand);
      await _context.SaveChangesAsync();

      return newBrand;
    }

    public async Task Update(int id, [FromBody] BrandDTO brandDTO)
    {
      var existingBrand = await GetByID(id);

      if (existingBrand is not null)
      {
      existingBrand.Name = brandDTO.Name;
      existingBrand.Logo = brandDTO.Logo;
      if (brandDTO.IsAvailable) {
        existingBrand.IsAvailable = "true";
      } else {
        existingBrand.IsAvailable = "false";
      }

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var brandToDelete = await GetByID(id);

      if(brandToDelete is not null)
      {
        _context.Brands.Remove(brandToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Brands.CountAsync();
    }

    public async Task<bool> IsBrandNameUnique(string brandName)
    {
    var brands = await _context.Brands.AsNoTracking().ToListAsync();
    return brands.Any(b => string.Equals(b.Name, brandName, StringComparison.OrdinalIgnoreCase));
    }
    }
}
