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

        [Required(ErrorMessage = "Enter an eye catching Title for your Ad.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Which type of vehicle is this?.")]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Vehicle Brand is required.")]
        [Display(Name = "Vehicle Brand")]
        public int VehicleBrandID { get; set; }

        [Required(ErrorMessage = "In which year was this vehicle was registered.")]
        [Display(Name = "Registration Year")]
        public int RegistrationYear { get; set; }

        [Required(ErrorMessage = "How many Kilometeres has this vehicle driven.")]
        [Display(Name = "Driven Kilometer")]
        public int DrivenKilometers { get; set; }

        [Required(ErrorMessage = "Color of this vehcile.")]
        [Display(Name = "Color")]
        public string Color { get; set; }
        
        [Display(Name = "Insurance")]
        public Nullable<bool> Insurance { get; set; }

        [Required(ErrorMessage = "What is the current condition of Vehicle.")]
        [Display(Name = "Condition")]
        public string Condition { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select Currency.")]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "How much price for this vehicle?")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Country details is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select a state that belong to the selected country.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Which city this vehicle is being sold in?")]
        [Display(Name = "City")]
        public int CityID { get; set; }
        
        public ICollection<int> ImageIDs { get; set; }

        [Required(ErrorMessage = "Seller details are must required.")]
        [Display(Name = "Seller")]
        public int SellerID { get; set; }

        [Required(ErrorMessage = "Posting Time is required.")]
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
