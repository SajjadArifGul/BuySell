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
using BuySell.Contracts.Repositories;
using BuySell.DAL.Repository;

namespace BuySell.WebUI.Areas.Dashboard.Controllers
{
    public class BikesController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Bike> Bikes;

        public BikesController()
        {
            Bikes = new BikesRepository(myDataContext);
        }

        // GET: Dashboard/Bikes
        public ActionResult Index()
        {
            var bikes = Bikes.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(50).ToList();
            return View(bikes);
        }

        // GET: Dashboard/Bikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = Bikes.GetByID(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // POST: Dashboard/Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = Bikes.GetByID(id);
            Bikes.Delete(bike);
            Bikes.Commit();
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
