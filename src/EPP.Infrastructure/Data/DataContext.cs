using api.Models;
using api.Models.Entities;
using api.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Administrator> Administrators => Set<Administrator>();
    }
}
