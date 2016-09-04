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
using Microsoft.AspNet.Identity;

namespace BuySell.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //instead of showing him bad request, why not show user, his own profile :D
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                string CurrentUserName = User.Identity.GetUserName();
                id = db.Sellers.Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                if (id==null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }

            return View(seller);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CityID = new SelectList(db.Cities.Where(s => s.StateID == seller.StateID), "ID", "Name", seller.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", seller.CountryID);
            ViewBag.StateID = new SelectList(db.States.Where(s=>s.CountryID == seller.CountryID), "ID", "Name", seller.StateID);
            return View(seller);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Username,Name,CountryID,StateID,CityID,MobileNumber,JoinDate")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", seller.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", seller.CountryID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", seller.StateID);
            return View(seller);
        }

        // GET: Profile/Delete/5
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

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
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
