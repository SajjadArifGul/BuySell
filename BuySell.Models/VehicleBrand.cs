using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class VehicleBrand
    {

        [Display(Name = "Vehicle Brand ID")]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Vehicle Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
       
        public byte[] Image { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
