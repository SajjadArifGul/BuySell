using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Condition
    {
            [Display(Name = "Condition ID")]
            public int ID { get; set; }

            [Required(ErrorMessage = "Condition Type is required.")]
            [Display(Name = "Condition Type")]
            public string ConditionType { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }
    }
}
