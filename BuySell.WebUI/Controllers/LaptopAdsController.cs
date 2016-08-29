using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuySell.DAL.Data;
using BuySell.WebUI.Models;
using BuySell.Contracts.Repositories;
using BuySell.Models;
using BuySell.DAL.Repository;
using System.IO;
using Microsoft.AspNet.Identity;

namespace BuySell.WebUI.Controllers
{
    public class LaptopAdsController : Controller
    {
        // GET: LaptopAds
        public ActionResult Index()
        {
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());
            List<Laptop> LaptopList = Laptops.GetAll().ToList();

            List<LaptopAdViewModel> laptopAdViewModels = new List<LaptopAdViewModel>();

            foreach (Laptop laptop in LaptopList)
            {
                LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

                laptopAdViewModel.ID = laptop.ID;
                laptopAdViewModel.Title = laptop.Ad.Title;
                laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
                laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
                laptopAdViewModel.Ram = laptop.Ram;
                laptopAdViewModel.Processor = laptop.Processor;
                laptopAdViewModel.HardDisk = laptop.HardDisk;
                laptopAdViewModel.Condition = laptop.Ad.Condition;
                laptopAdViewModel.Description = laptop.Ad.Description;
                laptopAdViewModel.Currency = laptop.Ad.Currency;
                laptopAdViewModel.Price = laptop.Ad.Price;
                laptopAdViewModel.Country = laptop.Ad.Country;
                laptopAdViewModel.State = laptop.Ad.State;
                laptopAdViewModel.City = laptop.Ad.City;
                laptopAdViewModel.Seller = laptop.Ad.Seller;
                laptopAdViewModel.Images = laptop.Ad.Images;

                laptopAdViewModels.Add(laptopAdViewModel);
            }

            return View(laptopAdViewModels);

        }

        // GET: LaptopAds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());
            Laptop laptop = Laptops.GetByID(id);

            if (laptop == null)
            {
                return HttpNotFound();
            }

            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
            laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
            laptopAdViewModel.Ram = laptop.Ram;
            laptopAdViewModel.Processor = laptop.Processor;
            laptopAdViewModel.HardDisk = laptop.HardDisk;
            laptopAdViewModel.Condition = laptop.Ad.Condition;
            laptopAdViewModel.Description = laptop.Ad.Description;
            laptopAdViewModel.Currency = laptop.Ad.Currency;
            laptopAdViewModel.Price = laptop.Ad.Price;
            laptopAdViewModel.Country = laptop.Ad.Country;
            laptopAdViewModel.State = laptop.Ad.State;
            laptopAdViewModel.City = laptop.Ad.City;
            laptopAdViewModel.Seller = laptop.Ad.Seller;
            laptopAdViewModel.PostingTime = laptop.Ad.PostingTime;
            laptopAdViewModel.Images = laptop.Ad.Images;


            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Create
        public ActionResult Create()
        {
            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(new DataContext());
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(new DataContext());
            IRepositoryBase<Country> Countries = new CountriesRepository(new DataContext());
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(new DataContext());


            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name");
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType");
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name");
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name");

            return View(new LaptopAdViewModel());
        }

        // POST: LaptopAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,Ram,Processor,HardDisk,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID")] LaptopAdViewModel laptopAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                IRepositoryBase<Ad> Ads = new AdsRepository(new DataContext());

                var ad = new Ad();

                ad.Title = laptopAdViewModel.Title;
                ad.ConditionID = laptopAdViewModel.ConditionID;
                ad.Description = laptopAdViewModel.Description;
                ad.CurrencyID = laptopAdViewModel.CurrencyID;
                ad.Price = laptopAdViewModel.Price;
                ad.CountryID = laptopAdViewModel.CountryID;
                ad.StateID = laptopAdViewModel.StateID;
                ad.CityID = laptopAdViewModel.CityID;

                IRepositoryBase<Seller> Sellers = new SellersRepository(new DataContext());

                string CurrentUserName = User.Identity.GetUserName();

                int AdSellerID = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                //ad.SellerID = Sellers.GetByID(CurrentUserID).ID; //Get this from Current User
                ad.SellerID = AdSellerID;

                ad.Slug = laptopAdViewModel.Title.Replace(' ', '-');

                ad.PostingTime = DateTime.Now;

                Ads.Insert(ad);

                Ads.Commit();

                //if (ImageFile != null && ImageFile.ContentLength > 0)
                //{
                //    var uploadDir = "~/images";
                //    var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageFile.FileName);
                //    ImageFile.SaveAs(imagePath);

                //    //var image = new Image
                //    //{
                //    //    Path = imagePath
                //    //    //AdID = ad.ID ////I think we dont need it
                //    //};

                //    var image = new Image();
                //    image.Path = imagePath;
                //    image.AdID = ad.ID;

                //    IRepositoryBase<Image> Images = new ImagesRepository(new DataContext());

                //    Images.Insert(image);
                //    Images.Commit();

                //    //ad.Images = new List<Image>();
                //    //ad.Images.Add(image);
                //}

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadDir = "~/images";
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    ImageFile.SaveAs(ImagePath);

                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    IRepositoryBase<Image> Images = new ImagesRepository(new DataContext());

                    Images.Insert(image);
                    Images.Commit();
                }

                IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());

                var laptop = new Laptop();

                laptop.AccessoryBrandID = laptopAdViewModel.AccessoryBrandID;
                laptop.OperatingSystem = laptopAdViewModel.OperatingSystem;
                laptop.Ram = laptopAdViewModel.Ram;
                laptop.Processor = laptopAdViewModel.Processor;
                laptop.HardDisk = laptopAdViewModel.HardDisk;
                laptop.AdID = ad.ID;

                Laptops.Insert(laptop);
                Laptops.Commit();

                return RedirectToAction("Index");
            }

            //ViewBag.AccessoryBrandID = new SelectList(db.AccessoryBrands, "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            //ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", laptopAdViewModel.CityID);
            //ViewBag.ConditionID = new SelectList(db.Conditions, "ID", "ConditionType", laptopAdViewModel.ConditionID);
            //ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", laptopAdViewModel.CountryID);
            //ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", laptopAdViewModel.CurrencyID);
            //ViewBag.StateID = new SelectList(db.States, "ID", "Name", laptopAdViewModel.StateID);

            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(new DataContext());
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(new DataContext());
            IRepositoryBase<Country> Countries = new CountriesRepository(new DataContext());
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(new DataContext());


            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name");
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType");
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name");
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name");
            
            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());
            Laptop laptop = Laptops.GetByID(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }

            //ViewBag.AccessoryBrandID = new SelectList(db.AccessoryBrands, "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            //ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", laptopAdViewModel.CityID);
            //ViewBag.ConditionID = new SelectList(db.Conditions, "ID", "ConditionType", laptopAdViewModel.ConditionID);
            //ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", laptopAdViewModel.CountryID);
            //ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", laptopAdViewModel.CurrencyID);
            //ViewBag.StateID = new SelectList(db.States, "ID", "Name", laptopAdViewModel.StateID);

            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
            laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
            laptopAdViewModel.Ram = laptop.Ram;
            laptopAdViewModel.Processor = laptop.Processor;
            laptopAdViewModel.HardDisk = laptop.HardDisk;
            laptopAdViewModel.ConditionID = laptop.Ad.ConditionID;
            laptopAdViewModel.Description = laptop.Ad.Description;
            laptopAdViewModel.CurrencyID = laptop.Ad.CurrencyID;
            laptopAdViewModel.Price = laptop.Ad.Price;
            laptopAdViewModel.CountryID = laptop.Ad.CountryID;
            laptopAdViewModel.StateID = laptop.Ad.StateID;
            laptopAdViewModel.CityID = laptop.Ad.CityID;
            laptopAdViewModel.SellerID = laptop.Ad.SellerID;



            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(new DataContext());
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(new DataContext());
            IRepositoryBase<Country> Countries = new CountriesRepository(new DataContext());
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(new DataContext());

            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name", laptop.AccessoryBrandID);
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType", laptop.Ad.ConditionID);
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name", laptop.Ad.CountryID);
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name", laptop.Ad.CurrencyID);

            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,Ram,Processor,HardDisk,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID")] LaptopAdViewModel laptopAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                IRepositoryBase<Ad> Ads = new AdsRepository(new DataContext());

                var ad = new Ad();
                ad.ID = laptopAdViewModel.ID;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadDir = "~/images";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageFile.FileName);
                    ImageFile.SaveAs(imagePath);

                    var image = new Image
                    {
                        Path = imagePath,
                        AdID = ad.ID ////I think we dont need it
                    };

                    //ad.Images = new List<Image>();
                    //ad.Images.Add(image);
                }

                ad.Title = laptopAdViewModel.Title;
                ad.ConditionID = laptopAdViewModel.ConditionID;
                ad.Description = laptopAdViewModel.Description;
                ad.CurrencyID = laptopAdViewModel.CurrencyID;
                ad.Price = laptopAdViewModel.Price;
                ad.CountryID = laptopAdViewModel.CountryID;
                ad.StateID = laptopAdViewModel.StateID;
                ad.CityID = laptopAdViewModel.CityID;

                IRepositoryBase<Seller> Sellers = new SellersRepository(new DataContext());

                string CurrentUserID = User.Identity.GetUserId();

                ad.SellerID = Sellers.GetByID(CurrentUserID).ID; //Get this from Current User

                ad.Slug = laptopAdViewModel.Title.Replace(' ', '-');

                ad.PostingTime = DateTime.Now;

                Ads.Update(ad);

                Ads.Commit();

                IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());

                var laptop = new Laptop();

                laptop.ID = laptopAdViewModel.ID;
                laptop.AccessoryBrandID = laptopAdViewModel.AccessoryBrandID;
                laptop.OperatingSystem = laptopAdViewModel.OperatingSystem;
                laptop.Ram = laptopAdViewModel.Ram;
                laptop.Processor = laptopAdViewModel.Processor;
                laptop.HardDisk = laptopAdViewModel.HardDisk;
                laptop.AdID = ad.ID;

                Laptops.Update(laptop);
                Laptops.Commit();

                
                return RedirectToAction("Index");
            }
            //ViewBag.AccessoryBrandID = new SelectList(db.AccessoryBrands, "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            //ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", laptopAdViewModel.CityID);
            //ViewBag.ConditionID = new SelectList(db.Conditions, "ID", "ConditionType", laptopAdViewModel.ConditionID);
            //ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", laptopAdViewModel.CountryID);
            //ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", laptopAdViewModel.CurrencyID);
            //ViewBag.StateID = new SelectList(db.States, "ID", "Name", laptopAdViewModel.StateID);


            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(new DataContext());
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(new DataContext());
            IRepositoryBase<Country> Countries = new CountriesRepository(new DataContext());
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(new DataContext());


            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType", laptopAdViewModel.ConditionID);
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name", laptopAdViewModel.CountryID);
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name", laptopAdViewModel.CurrencyID);


            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());
            Laptop laptop = Laptops.GetByID(id);
            if (laptop == null)
            {
                return HttpNotFound();
            }

            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
            laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
            laptopAdViewModel.Ram = laptop.Ram;
            laptopAdViewModel.Processor = laptop.Processor;
            laptopAdViewModel.HardDisk = laptop.HardDisk;
            laptopAdViewModel.ConditionID = laptop.Ad.ConditionID;
            laptopAdViewModel.Description = laptop.Ad.Description;
            laptopAdViewModel.CurrencyID = laptop.Ad.CurrencyID;
            laptopAdViewModel.Price = laptop.Ad.Price;
            laptopAdViewModel.CountryID = laptop.Ad.CountryID;
            laptopAdViewModel.StateID = laptop.Ad.StateID;
            laptopAdViewModel.CityID = laptop.Ad.CityID;
            laptopAdViewModel.SellerID = laptop.Ad.SellerID;

            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //LaptopAdViewModel laptopAdViewModel = db.LaptopAdViewModels.Find(id);
            //db.LaptopAdViewModels.Remove(laptopAdViewModel);

            //db.SaveChanges();

            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());
            Laptop laptop = Laptops.GetByID(id);

            Laptops.Delete(id);
            Laptops.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                IRepositoryBase<Laptop> Laptops = new LaptopsRepository(new DataContext());

                Laptops.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
