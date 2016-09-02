using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Models
{
    public class HomeViewModel
    {
        public int ID { get; set; }
        
        //Lists of Ads to be shown on HomePage
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}