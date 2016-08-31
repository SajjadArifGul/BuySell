using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Review
    {
        public int ID { get; set; }

        [MinLength(100)]
        [Required]
        [Display(Name = "Review is Required.")]
        public string Content { get; set; }
        public int AdID { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
