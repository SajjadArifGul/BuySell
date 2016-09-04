using BuySell.DAL.Data;
using BuySell.WebUI.Areas.Dashboard.Models;
using BuySell.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<UserRolesViewModel> userRolesViewModels = new List<UserRolesViewModel>();

            var users = appcontext.Users;
            var rolesfromDB = appcontext.Roles;
            
            foreach (var user in users)
            {
                List<string> UserRoles = new List<string>();

                var ThisUserRoles = user.Roles;

                foreach (var Role in ThisUserRoles)
                {
                    foreach (var role in rolesfromDB)
                    {
                        if (Role.RoleId == role.Id)
                        {
                            UserRoles.Add(role.Name);
                        }
                    }
                }

                UserRolesViewModel userRoleViewModel = new UserRolesViewModel();

                userRoleViewModel.User = user;
                userRoleViewModel.RoleNames = UserRoles;

                userRolesViewModels.Add(userRoleViewModel);
            }

            return View(userRolesViewModels);
        }

        // GET: Dashboard/Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Roles/Create
        public ActionResult Create()
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

        // GET: Dashboard/Roles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
    }
}
