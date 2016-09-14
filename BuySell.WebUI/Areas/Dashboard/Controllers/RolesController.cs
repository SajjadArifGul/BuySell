using BuySell.DAL.Data;
using BuySell.WebUI.Areas.Dashboard.Models;
using BuySell.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BuySell.WebUI.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        //Since Roles are manage by Microsoft Owin & Identity shit. We need a different datacontext for this.
        ApplicationDbContext appcontext = new ApplicationDbContext();
        
        // GET: Dashboard/Roles
        public ActionResult Index()
        {
            UsersRolesViewModel UsersWithRoles = new UsersRolesViewModel();

            var allUsers = appcontext.Users.ToList();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appcontext));

            List<ApplicationUser> Adminstrators = new List<ApplicationUser>();
            List<ApplicationUser> Managers = new List<ApplicationUser>();
            List<ApplicationUser> Sellers = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    Adminstrators.Add(user);
                }
                else if (userManager.IsInRole(user.Id, "Manager"))
                {
                    Managers.Add(user);
                }
                else if (userManager.IsInRole(user.Id, "Seller"))
                {
                    Sellers.Add(user);
                }
            }

            UsersWithRoles.Adminstrators = Adminstrators;
            UsersWithRoles.Managers = Managers;
            UsersWithRoles.Sellers = Sellers;

            return View(UsersWithRoles);
        }

        // GET: Dashboard/Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = appcontext.Users.Where(i => i.Id == id).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appcontext));

            List<string> userRoles = userManager.GetRoles(user.Id).ToList();

            UserRolesViewModel userRoleViewModel = new UserRolesViewModel();

            userRoleViewModel.User = user;
            userRoleViewModel.UserRoles = userRoles;

            var roles = appcontext.Roles.ToList();

            List<string> AllRoles = new List<string>();

            foreach (var role in roles)
            {
                AllRoles.Add(role.Name);
            }

            userRoleViewModel.AllRoles = AllRoles;
            return View(userRoleViewModel);
        }

        // GET: Dashboard/Roles/Create
        public ActionResult AddNewRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewRole(string RoleName)
        {
            if (!string.IsNullOrEmpty(RoleName) && !string.IsNullOrWhiteSpace(RoleName) && RoleName.Length > 5)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appcontext));

                // check if this doesnt already exists    
                if (!roleManager.RoleExists(RoleName))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = RoleName;
                    roleManager.Create(role);
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Dashboard/Roles/Create
        public ActionResult DeleteRole()
        {
            var Roles = appcontext.Roles.ToList();

            var AllRoleNames = (from r in Roles
                     select new SelectListItem { Text = r.Name, Value = r.Name });

            ViewBag.AllRoles = AllRoleNames;

            return View();
        }
        [HttpPost]
        public ActionResult DeleteRole(string RoleName)
        {
            if (RoleName.Equals("Admin") || RoleName.Equals("Manager") || RoleName.Equals("Seller"))
            {
                //These are must roles & no should delete it
                return RedirectToAction("Index");
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appcontext));

            // check if this exists    
            if (roleManager.RoleExists(RoleName))
            {
                var Role = appcontext.Roles.Where(r => r.Name == RoleName).FirstOrDefault();

                appcontext.Roles.Remove(Role);
                appcontext.SaveChanges();

                //I should also delete Roles from Users too here.
                var allUsers = appcontext.Users.ToList();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appcontext));

                foreach (var user in allUsers)
                {
                    if (userManager.IsInRole(user.Id, RoleName))
                    {
                        userManager.RemoveFromRole(user.Id, RoleName);
                    }
                }
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string id, string RoleName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = appcontext.Users.Where(i => i.Id == id).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            List<string> AllowedRoles = new List<string>();

            var RolesFromDB = appcontext.Roles.ToList();

            foreach (var Role in RolesFromDB)
            {
                AllowedRoles.Add(Role.Name);
            }

            if (!AllowedRoles.Contains(RoleName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appcontext));

            userManager.AddToRole(user.Id, RoleName);

            //Now return the user back to the details of this ad
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleFromUser(string id, string RoleName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = appcontext.Users.Where(i => i.Id == id).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appcontext));

            List<string> userRoles = userManager.GetRoles(user.Id).ToList();

            if (!userRoles.Contains(RoleName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userManager.RemoveFromRole(user.Id, RoleName);

            return RedirectToAction("Index");
        }

    }
}
