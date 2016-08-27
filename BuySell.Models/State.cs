using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class State
    {
        [Display(Name = "State ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "State Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select Country where this state belongs.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
