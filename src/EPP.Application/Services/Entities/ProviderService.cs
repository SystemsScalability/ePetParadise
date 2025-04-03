using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ProviderService
    {
        private readonly DataContext _context;

        public ProviderService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Provider>> GetAll()
    {
      return await _context.Providers.ToListAsync();
    }

    public async Task<Provider?> GetByID(int id)
    {
      return await _context.Providers.FindAsync(id);
    }

    public async Task<Provider> Create(ProviderDTO newProviderDTO)
    {
      if (await IsBrandNameUnique(newProviderDTO.Name))
      {
        var provider = new Provider();
        provider.Name = "error_409_validations";
        return provider;
      }

      var newProvider = new Provider();
      newProvider.ProviderID = await GetCount() + 1;
      newProvider.Name = newProviderDTO.Name;
      newProvider.Nationality = newProviderDTO.Nationality;
      if (newProviderDTO.IsAvailable) {
        newProvider.IsAvailable = "true";
      } else {
        newProvider.IsAvailable = "false";
      }
      

      _context.Providers.Add(newProvider);
      await _context.SaveChangesAsync();

      return newProvider;
    }

    public async Task Update(int id, ProviderDTO providerDTO)
    {
      var existingProvider = await GetByID(id);

      if (existingProvider is not null)
      {
      existingProvider.Name = providerDTO.Name;
      existingProvider.Nationality = providerDTO.Nationality;
      if (providerDTO.IsAvailable) {
        existingProvider.IsAvailable = "true";
      } else {
        existingProvider.IsAvailable = "false";
      }

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var providerToDelete = await GetByID(id);

      if(providerToDelete is not null)
      {
        _context.Providers.Remove(providerToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Providers.CountAsync();
    }

    public async Task<bool> IsBrandNameUnique(string providerName)
    {
    var providers = await _context.Providers.AsNoTracking().ToListAsync();
    return providers.Any(b => string.Equals(b.Name, providerName, StringComparison.OrdinalIgnoreCase));
    }
    }
}
