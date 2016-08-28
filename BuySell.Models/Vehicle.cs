using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Which type of vehicle is this?.")]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeID { get; set; }

        [Required(ErrorMessage = "Vehicle Brand is required.")]
        [Display(Name = "Vehicle Brand")]
        public int VehicleBrandID { get; set; }

        [Required(ErrorMessage = "In which year was this vehicle was registered.")]
        [Display(Name = "Registration Year")]
        public int YearID { get; set; }

        [Required(ErrorMessage = "How many Kilometeres has this vehicle driven.")]
        [Display(Name = "Driven Kilometer")]
        public int DrivenKilometers { get; set; }

        [Required(ErrorMessage = "Color of this vehcile.")]
        [Display(Name = "Color")]
        public int ColorID { get; set; }

        [Display(Name = "Insurance")]
        public Nullable<bool> Insurance { get; set; }

        [Required(ErrorMessage = "Ad ID.")]
        [Display(Name = "Ad")]
        public int AdID { get; set; }

        public virtual VehicleBrand VehicleBrand { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual Year Year { get; set; }
        public virtual Color Color { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
