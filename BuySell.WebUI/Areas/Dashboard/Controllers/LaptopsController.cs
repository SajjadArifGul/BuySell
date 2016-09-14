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
    public class LaptopsController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Laptop> Laptops;

        public LaptopsController()
        {
            Laptops = new LaptopsRepository(myDataContext);
        }

        // GET: Dashboard/Laptops
        public ActionResult Index()
        {
            var laptops = Laptops.GetAll().OrderByDescending(l => l.Ad.PostingTime).Take(50).ToList();
            
            return View(laptops);
        }

        // GET: Dashboard/Laptops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laptop laptop = Laptops.GetByID(id);
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
            Laptop laptop = Laptops.GetByID(id);
            Laptops.Delete(laptop);
            Laptops.Commit();
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
