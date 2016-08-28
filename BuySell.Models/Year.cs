using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Year
    {
            [Display(Name = "Year ID")]
            public int ID { get; set; }

            [Required(ErrorMessage = "Year is required.")]
            [Display(Name = "Year")]
            public string YearNo { get; set; }
    }
}
