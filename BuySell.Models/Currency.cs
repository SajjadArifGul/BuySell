using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Currency
    {
        [Display(Name = "Currency ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Currency Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country Name is required to differentiate between different country currencies with same name.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }
        
        [Display(Name = "Symbol")]
        public string Symbol { get; set; }
        
        [Display(Name = "ISO Code")]
        public string ISOCode { get; set; }

        public virtual Country Country { get; set; }

    }
}
