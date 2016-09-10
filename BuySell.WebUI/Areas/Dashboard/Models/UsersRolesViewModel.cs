using BuySell.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Areas.Dashboard.Models
{
    public class UsersRolesViewModel
    {
        public ICollection<ApplicationUser> Adminstrators { get; set; }
        public ICollection<ApplicationUser> Managers { get; set; }
        public ICollection<ApplicationUser> Sellers { get; set; }
    }
}