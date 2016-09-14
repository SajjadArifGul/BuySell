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
using BuySell.Contracts.Repositories;
using BuySell.DAL.Repository;

namespace BuySell.WebUI.Areas.Dashboard.Controllers
{
    public class SellersController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Seller> Sellers;

        public SellersController()
        {
            Sellers = new SellersRepository(myDataContext);
        }

        // GET: Dashboard/Sellers
        public ActionResult Index()
        {
            var sellers = Sellers.GetAll().OrderByDescending(s => s.JoinDate).Take(50).ToList();
            return View(sellers);
        }

        // GET: Dashboard/Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = Sellers.GetByID(id);
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
            Seller seller = Sellers.GetByID(id);
            Sellers.Delete(seller);
            Sellers.Commit();

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
                myDataContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
