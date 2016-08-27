using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Country
    {
        [Display(Name = "Country ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Country Name is must required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
