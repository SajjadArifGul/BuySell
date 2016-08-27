using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class AccessoryBrand
    {
        [Display(Name = "Accessory Brand ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Image")]
        public byte[] Image { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
