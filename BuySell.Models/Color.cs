using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Color
    {
        [Display(Name = "Color ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Color Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Color Hex Code is required.")]
        [Display(Name = "Hex Code")]
        public string HexCode { get; set; }

        [Required(ErrorMessage = "RGB Red is required.")]
        [Display(Name = "RGB Red")]
        public string RGBRed { get; set; }

        [Required(ErrorMessage = "RGB Green is required.")]
        [Display(Name = "RGB Green")]
        public string RGBGreen { get; set; }

        [Required(ErrorMessage = "RGB Blue is required.")]
        [Display(Name = "RGB Blue")]
        public string RGBBlue { get; set; }
    }
}
