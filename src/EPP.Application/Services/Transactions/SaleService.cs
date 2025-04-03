using api.Data;
using api.DTOs.Entities;
using api.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class SaleService
    {
        private readonly DataContext _context;

        public SaleService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAll()
    {
      return await _context.Sales.ToListAsync();
    }

    public async Task<Sale?> GetByID(int id)
    {
      return await _context.Sales.FindAsync(id);
    }

    public async Task<Sale> Create(SaleDTO newSaleDTO)
    {
      var newSale = new Sale();
      newSale.SaleID = await GetCount() + 1;
      newSale.ZipCode = newSaleDTO.ZipCode;
      newSale.Cvv = newSaleDTO.Cvv;
      newSale.CardNumber = newSaleDTO.CardNumber;
      newSale.Date = newSaleDTO.Date;
      newSale.FinalPrice = newSaleDTO.FinalPrice;
      newSale.UserID = newSaleDTO.UserID;
      newSale.IsAvailable = newSaleDTO.IsAvailable;
      
      _context.Sales.Add(newSale);
      await _context.SaveChangesAsync();

      return newSale;
    }

    public async Task Update(int id, SaleDTO saleDTO)
    {
      var existingSale = await GetByID(id);

      if (existingSale is not null)
      {
      existingSale.ZipCode = saleDTO.ZipCode;
      existingSale.Cvv = saleDTO.Cvv;
      existingSale.CardNumber = saleDTO.CardNumber;
      existingSale.Date = saleDTO.Date;
      existingSale.FinalPrice = saleDTO.FinalPrice;
      existingSale.UserID = saleDTO.UserID;
      existingSale.IsAvailable = saleDTO.IsAvailable;

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var saleToDelete = await GetByID(id);

      if(saleToDelete is not null)
      {
        _context.Sales.Remove(saleToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Sales.CountAsync();
    }
    }
}
