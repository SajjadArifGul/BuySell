using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class VehicleAd
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        [Required]
        [Display(Name = "Vehicle Brand")]
        public int VehicleBrandID { get; set; }

        [Required]
        [Display(Name = "Registration Year")]
        public int RegistrationYear { get; set; }

        [Required]
        [Display(Name = "Driven Kilometer")]
        public int DrivenKilometers { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }
        
        [Display(Name = "Insurance")]
        public Nullable<bool> Insurance { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public string Condition { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityID { get; set; }
        
        [Display(Name = "Images")]
        public ICollection<int> ImageIDs { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public int SellerID { get; set; }

        [Required]
        [Display(Name = "Posting Time")]
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
