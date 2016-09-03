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

namespace BuySell.WebUI.Areas.Dashboard.Controllers
{
    public class LaptopsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Dashboard/Laptops
        public ActionResult Index()
        {
            var laptops = db.Laptops.Include(l => l.AccessoryBrand).Include(l => l.Ad);
            return View(laptops.Take(50).ToList().OrderByDescending(a=>a.Ad.PostingTime));
        }

        // GET: Dashboard/Laptops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = db.Laptops.Find(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // POST: Dashboard/Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laptop laptop = db.Laptops.Find(id);
            db.Laptops.Remove(laptop);
            db.SaveChanges();
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
