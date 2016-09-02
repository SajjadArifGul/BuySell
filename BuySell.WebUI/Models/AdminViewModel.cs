using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Models
{
    public class AdminViewModel
    {
        public int ID { get; set; }

        public virtual ICollection<BikeAdViewModel> Bikes { get; set; }
        public virtual ICollection<LaptopAdViewModel> Laptops { get; set; }
        public virtual ICollection<CellPhoneAdViewModel> CellPhones { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}