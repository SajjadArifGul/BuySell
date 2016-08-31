using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Ad
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter an eye catching Title for your Ad.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

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

        [Required(ErrorMessage = "Seller Details are must required.")]
        [Display(Name = "Seller")]
        public int SellerID { get; set; }

        [Required(ErrorMessage = "Slug is shown in URL of Browser.")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Posting Time is required.")]
        [Display(Name = "Posting Time")]
        public System.DateTime PostingTime { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
