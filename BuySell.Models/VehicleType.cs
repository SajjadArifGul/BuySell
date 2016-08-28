using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class VehicleType
    {
        [Display(Name = "Vehicle Type ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required.")]
        [Display(Name = "Vehicle Type")]
        public string Type { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
