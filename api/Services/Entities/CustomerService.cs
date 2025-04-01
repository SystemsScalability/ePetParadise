using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
      return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByID(int id)
    {
      return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer> Create(CustomerDTO newCustomerDTO)
    {
      if (await IsBrandNameUnique(newCustomerDTO.Name))
      {
        var customer = new Customer();
        customer.Name = "name_error_409_validations";
        return customer;
      }

      if (await IsEmailUnique(newCustomerDTO.Email))
      {
        var customer = new Customer();
        customer.Email = "email_error_409_validations";
        return customer;
      }

      var newCustomer = new Customer();
      newCustomer.CustomerID = await GetCount() + 1;
      newCustomer.Email = newCustomerDTO.Email;
      newCustomer.Name = newCustomerDTO.Name;
      newCustomer.Password = newCustomerDTO.Password;
      newCustomer.RegionID = newCustomerDTO.RegionID;
      newCustomer.Nit = newCustomerDTO.Nit;
      if (newCustomerDTO.IsAvailable) {
        newCustomer.IsAvailable = "true";
      } else {
        newCustomer.IsAvailable = "false";
      }

      _context.Customers.Add(newCustomer);
      await _context.SaveChangesAsync();

      return newCustomer;
    }

    public async Task Update(int id, CustomerDTO customerDTO)
    {
      var existingCustomer = await GetByID(id);

      if (existingCustomer is not null)
      {
      existingCustomer.Email = customerDTO.Name;
      existingCustomer.Name = customerDTO.Name;
      existingCustomer.Password = customerDTO.Password;
      existingCustomer.RegionID = customerDTO.RegionID;
      existingCustomer.Nit = customerDTO.Nit;
      if (customerDTO.IsAvailable) {
        existingCustomer.IsAvailable = "true";
      } else {
        existingCustomer.IsAvailable = "false";
      }

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var customerToDelete = await GetByID(id);

      if(customerToDelete is not null)
      {
        _context.Customers.Remove(customerToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Customers.CountAsync();
    }

    public async Task<bool> IsBrandNameUnique(string customerName)
    {
    var customers = await _context.Customers.AsNoTracking().ToListAsync();
    return customers.Any(b => string.Equals(b.Name, customerName, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<bool> IsEmailUnique(string customerEmail)
    {
    var customers = await _context.Customers.AsNoTracking().ToListAsync();
    return customers.Any(b => string.Equals(b.Email, customerEmail, StringComparison.OrdinalIgnoreCase));
    }
    }
}
