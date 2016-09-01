using BuySell.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Models
{
    public class LaptopAdViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter an eye catching Title for your Ad.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Laptop Brand is required.")]
        [Display(Name = "Laptop Brand")]
        public int AccessoryBrandID { get; set; }

        [Required(ErrorMessage = "Operating System is required.")]
        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }

        [Required(ErrorMessage = "Ram is required.")]
        [Display(Name = "Ram")]
        public string Ram { get; set; }

        [Required(ErrorMessage = "Processor is required.")]
        [Display(Name = "Processor")]
        public string Processor { get; set; }

        [Required(ErrorMessage = "Hard Disk is required.")]
        [Display(Name = "Hard Disk")]
        public string HardDisk { get; set; }

        [Required(ErrorMessage = "Current Condition details are required.")]
        [Display(Name = "Condition")]
        public int ConditionID { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select Currency.")]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "Enter Price.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter Country details.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select a Country State.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Select a City.")]
        [Display(Name = "City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Seller ID is a must required.")]
        [Display(Name = "Seller ID")]
        public int SellerID { get; set; }

        [Display(Name = "Posting Time")]
        public System.DateTime PostingTime { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 50)]
        public string Review { get; set; }

        public virtual AccessoryBrand AccessoryBrand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Seller Seller { get; set; }

        //Lists for DropDowns
        public virtual IEnumerable<AccessoryBrand> AccessoryBrandsList { get; set; }
        public virtual IEnumerable<Condition> ConditionsList { get; set; }
        public virtual IEnumerable<Currency> CurrenciesList { get; set; }
        public virtual IEnumerable<Country> CountriesList { get; set; }
        public virtual IEnumerable<State> StatesList { get; set; }
        public virtual IEnumerable<City> CitiesList { get; set; }
    }
}