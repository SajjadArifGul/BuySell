using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<AccessoryBrand> AccessoryBrands { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CellPhone> CellPhones { get; set; }
        public DbSet<Bike> Bikes { get; set; }
    }
}
