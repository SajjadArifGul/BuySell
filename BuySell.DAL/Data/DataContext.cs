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

        public DbSet<AccessoryAd> AccessoryAds { get; set; }
        public DbSet<AccessoryBrand> AccessoryBrands { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<VehicleAd> VehicleAds { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
    }
}
