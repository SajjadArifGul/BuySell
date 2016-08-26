using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class VehicleAd
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string VehicleType { get; set; }
        public int VehicleBrandID { get; set; }
        public int RegistrationYear { get; set; }
        public int DrivenKilometers { get; set; }
        public string Color { get; set; }
        public Nullable<bool> Insurance { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public int CurrencyID { get; set; }
        public decimal Price { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public ICollection<int> ImageIDs { get; set; }
        public int SellerID { get; set; }
        public System.DateTime PostingTime { get; set; }

        public virtual VehicleBrand VehicleBrand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
