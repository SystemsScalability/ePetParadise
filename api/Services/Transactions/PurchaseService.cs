using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class PurchaseService
    {
        private readonly DataContext _context;

        public PurchaseService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Purchase>> GetAll()
    {
      return await _context.Purchases.ToListAsync();
    }

    public async Task<Purchase?> GetByID(int id)
    {
      return await _context.Purchases.FindAsync(id);
    }

    public async Task<Purchase> Create(PurchaseDTO newPurchaseDTO)
    {
      var newPurchase = new Purchase();
      newPurchase.PurchaseID = await GetCount() + 1;
      newPurchase.TotalPrice = newPurchaseDTO.TotalPrice;
      newPurchase.ObtainedTaxes = newPurchaseDTO.ObtainedTaxes;
      newPurchase.ReportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");;
      newPurchase.ApplicationTax = 7.5M;
      newPurchase.DeliveryTime = newPurchaseDTO.DeliveryTime;
      newPurchase.LocalQuantity = newPurchaseDTO.LocalQuantity;
      newPurchase.ProductID = newPurchaseDTO.ProductID;
      newPurchase.UserID = newPurchaseDTO.UserID;
      if (newPurchaseDTO.IsAvailable) {
        newPurchase.IsAvailable = "true";
      } else {
        newPurchase.IsAvailable = "false";
      }
      
      _context.Purchases.Add(newPurchase);
      await _context.SaveChangesAsync();

      return newPurchase;
    }

    public async Task Update(int id, PurchaseDTO purchaseDTO)
    {
      var existingPurchase = await GetByID(id);

      if (existingPurchase is not null)
      {
      existingPurchase.TotalPrice = purchaseDTO.TotalPrice;
      existingPurchase.ObtainedTaxes = purchaseDTO.ObtainedTaxes;
      existingPurchase.DeliveryTime = purchaseDTO.DeliveryTime;
      existingPurchase.LocalQuantity =purchaseDTO.LocalQuantity;
      existingPurchase.ProductID = purchaseDTO.ProductID;
      existingPurchase.UserID = purchaseDTO.UserID;
      if (purchaseDTO.IsAvailable) {
        existingPurchase.IsAvailable = "true";
      } else {
        existingPurchase.IsAvailable = "false";
      }

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var purchaseToDelete = await GetByID(id);

      if(purchaseToDelete is not null)
      {
        _context.Purchases.Remove(purchaseToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Purchases.CountAsync();
    }
    }
}
