using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class State
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
