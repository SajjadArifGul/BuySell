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
    public class VehicleBrandsController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<VehicleBrand> VehicleBrands;

        public VehicleBrandsController()
        {
            VehicleBrands = new VehicleBrandsRepository(myDataContext);
        }
        
        // GET: Dashboard/VehicleBrands
        public ActionResult Index()
        {
            var vehicleBrands = VehicleBrands.GetAll().OrderBy(v => v.Name).ToList();
            return View(vehicleBrands);
        }

        // GET: Dashboard/VehicleBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = VehicleBrands.GetByID(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // GET: Dashboard/VehicleBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/VehicleBrands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                VehicleBrands.Insert(vehicleBrand);
                VehicleBrands.Commit();
                return RedirectToAction("Index");
            }

            return View(vehicleBrand);
        }

        // GET: Dashboard/VehicleBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = VehicleBrands.GetByID(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: Dashboard/VehicleBrands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Image,Description")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                VehicleBrands.Update(vehicleBrand);
                VehicleBrands.Commit();
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }

        // GET: Dashboard/VehicleBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand vehicleBrand = VehicleBrands.GetByID(id);
            if (vehicleBrand == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBrand);
        }

        // POST: Dashboard/VehicleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleBrand vehicleBrand = VehicleBrands.GetByID(id);
            VehicleBrands.Delete(vehicleBrand);
            VehicleBrands.Commit();
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
