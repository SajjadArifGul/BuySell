using BuySell.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuySell.WebUI.Areas.Dashboard.Models
{
    public class UserRolesViewModel
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<string> RoleNames { get; set; }
    }
}