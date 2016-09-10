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
        ApplicationDbContext appcontext = new ApplicationDbContext();

        DataContext context = new DataContext();
        
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
            return View();
        }

        // POST: Dashboard/Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Dashboard/Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

            //Now return the user back to the details of this ad
            return RedirectToAction("Index");
        }

    }
}
