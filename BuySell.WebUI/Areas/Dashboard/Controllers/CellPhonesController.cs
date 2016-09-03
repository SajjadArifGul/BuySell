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
    public class CellPhonesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Dashboard/CellPhones
        public ActionResult Index()
        {
            var cellPhones = db.CellPhones.Include(c => c.AccessoryBrand).Include(c => c.Ad);
            return View(cellPhones.ToList().Take(50).OrderByDescending(a=>a.Ad.PostingTime));
        }

        // GET: Dashboard/CellPhones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CellPhone cellPhone = db.CellPhones.Find(id);
            if (cellPhone == null)
            {
                return HttpNotFound();
            }
            return View(cellPhone);
        }

        // POST: Dashboard/CellPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CellPhone cellPhone = db.CellPhones.Find(id);
            db.CellPhones.Remove(cellPhone);
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
