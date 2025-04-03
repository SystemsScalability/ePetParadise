using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class RegionService
    {
        private readonly DataContext _context;

        public RegionService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Region>> GetAll()
    {
      return await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByID(int id)
    {
      return await _context.Regions.FindAsync(id);
    }

    public async Task<Region> Create(RegionDTO newRegionDTO)
    {
      var newRegion = new Region();
      newRegion.RegionID = await GetCount() + 1;
      newRegion.Name = newRegionDTO.Name;
      newRegion.MunicipalTax = newRegionDTO.MunicipalTax;
      newRegion.StatalTax = newRegionDTO.StatalTax;

      _context.Regions.Add(newRegion);
      await _context.SaveChangesAsync();

      return newRegion;
    }

    public async Task Update(int id, RegionDTO regionDTO)
    {
      var existingRegion = await GetByID(id);

      if (existingRegion is not null)
      {
      existingRegion.Name = regionDTO.Name;
      existingRegion.MunicipalTax = regionDTO.MunicipalTax;
      existingRegion.StatalTax = regionDTO.StatalTax;

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var regionToDelete = await GetByID(id);

      if(regionToDelete is not null)
      {
        _context.Regions.Remove(regionToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Regions.CountAsync();
    }
    }
}
