using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StateID { get; set; }

        public virtual State State { get; set; }
    }
}
