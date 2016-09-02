using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Models
{
    public class HomeViewModel
    {
        public int ID { get; set; }

        //Lists of Ads to be shown on HomePage
        public virtual ICollection<BikeAdViewModel> Bikes { get; set; }
        public virtual ICollection<LaptopAdViewModel> Laptops { get; set; }
        public virtual ICollection<CellPhoneAdViewModel> CellPhones { get; set; }

        //public virtual IEnumerable<Bike> Bikes { get; set; }
        //public virtual IEnumerable<Laptop> Laptops { get; set; }
        //public virtual IEnumerable<CellPhone> CellPhones { get; set; }
    }
}