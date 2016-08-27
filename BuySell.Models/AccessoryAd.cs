using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class AccessoryAd
    {
        [Display(Name = "Accessory Ad ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Ad Title is required.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Accessory Brand is required.")]
        [Display(Name = "Accessory Brand")]
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

        [Required(ErrorMessage = "Enter Condition Details.")]
        [Display(Name = "Condition")]
        public string Condition { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Must Select Currency.")]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "Enter Expected Price.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Country Detail is required to display Ad in that Country.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select State from Selected Country.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "City Details is must required.")]
        [Display(Name = "City")]
        public int CityID { get; set; }
        
        [Display(Name = "Images")]
        public ICollection<int> ImageIDs { get; set; }

        [Required(ErrorMessage = "Seller Details are must for contacting.")]
        [Display(Name = "Seller")]
        public int SellerID { get; set; }

        [Required(ErrorMessage = "Posting Time is required for relevance.")]
        [Display(Name = "Posting Time")]
        public System.DateTime PostingTime { get; set; }

        public virtual AccessoryBrand AccessoryBrand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual Seller Seller { get; set; }

    }
}
