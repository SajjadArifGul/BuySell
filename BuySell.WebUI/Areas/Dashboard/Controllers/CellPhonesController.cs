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
    public class CellPhonesController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<CellPhone> CellPhones;

        public CellPhonesController()
        {
            CellPhones = new CellPhonesRepository(myDataContext);
        }

        // GET: Dashboard/CellPhones
        public ActionResult Index()
        {
            var cellPhones = CellPhones.GetAll().OrderByDescending(c => c.Ad.PostingTime).Take(50).ToList();
            return View(cellPhones);
        }

        // GET: Dashboard/CellPhones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CellPhone cellPhone = CellPhones.GetByID(id);
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
            CellPhone cellPhone = CellPhones.GetByID(id);
            CellPhones.Delete(cellPhone);
            CellPhones.Commit();
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
