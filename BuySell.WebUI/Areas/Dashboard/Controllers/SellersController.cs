using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuySell.DAL.Data;
using BuySell.Models;
using BuySell.WebUI.Models;

namespace BuySell.WebUI.Areas.Dashboard.Controllers
{
    public class SellersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Dashboard/Sellers
        public ActionResult Index()
        {
            var sellers = db.Sellers.Include(s => s.City).Include(s => s.Country).Include(s => s.State);
            return View(sellers.ToList());
        }

        // GET: Dashboard/Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Dashboard/Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
            db.SaveChanges();

            // Now also delete the corresponding user from Users table.
            //bcz we have a shit of 2 different tables for users & sellers.

            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            var user = applicationDbContext.Users.Where(u => u.UserName == seller.Username).FirstOrDefault();
            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
