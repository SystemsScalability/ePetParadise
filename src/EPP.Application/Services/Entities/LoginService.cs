using api.Data;
using api.DTOs.Entities;
using api.Models;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Entities
{
    public class LoginService
    {
        private readonly DataContext _context;
        public LoginService(DataContext context) 
        {
            _context = context;
            InitializeAdminAsync().Wait();
        }

        public async Task<Administrator?> GetAdmin(AdministratorDTO admin)
        {
            return await _context.Administrators.
                      SingleOrDefaultAsync(x => x.Email == admin.Email && x.Password == admin.Password);
        }

        public async Task InitializeAdminAsync()
        {
            var existingAdmins = await _context.Administrators.ToListAsync();
            _context.Administrators.RemoveRange(existingAdmins);
            await _context.SaveChangesAsync();

            var administrator = new Administrator
                {
                    Name = "admin",
                    Email = "admin@admin.com",
                    AdminType = "administrator",
                    Password = "admin",
                    RegisterDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Nit = 123456789,
                    PhoneNumber = 987654321
                };  

            _context.Administrators.Add(administrator);
            await _context.SaveChangesAsync();
        }
    }
}