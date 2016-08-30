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
        DataContext myDataContext = new DataContext();

        // GET: LaptopAds
        public ActionResult Index()
        {
            //This is the main page for Laptop Ads.
            //we will get records from Database into this page.

            //Create repository of Laptop to get Laptop Ads.
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);

            //Get All Laptops as List
            List<Laptop> LaptopList = Laptops.GetAll().ToList();

            //There will be many Laptop Ads so we created a whole List of LaptopAdViewModel
            List<LaptopAdViewModel> laptopAdViewModels = new List<LaptopAdViewModel>();

            //Now Iterate over this List of laptops to populate LaptopAdVieqModels List
            foreach (Laptop laptop in LaptopList)
            {
                //New LaptopAdViewModel Object
                LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

                //populate this object from 
                laptopAdViewModel.ID = laptop.ID;
                laptopAdViewModel.Title = laptop.Ad.Title;
                laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
                laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
                laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
                laptopAdViewModel.Ram = laptop.Ram;
                laptopAdViewModel.Processor = laptop.Processor;
                laptopAdViewModel.HardDisk = laptop.HardDisk;
                laptopAdViewModel.Condition = laptop.Ad.Condition;
                laptopAdViewModel.ConditionID = laptop.Ad.ConditionID;
                laptopAdViewModel.Description = laptop.Ad.Description;
                laptopAdViewModel.Currency = laptop.Ad.Currency;
                laptopAdViewModel.CurrencyID = laptop.Ad.CurrencyID;
                laptopAdViewModel.Price = laptop.Ad.Price;
                laptopAdViewModel.Country = laptop.Ad.Country;
                laptopAdViewModel.CountryID = laptop.Ad.CountryID;
                laptopAdViewModel.StateID = laptop.Ad.StateID;
                laptopAdViewModel.State = laptop.Ad.State;
                laptopAdViewModel.CityID = laptop.Ad.CityID;
                laptopAdViewModel.City = laptop.Ad.City;
                laptopAdViewModel.SellerID = laptop.Ad.SellerID;
                laptopAdViewModel.Seller = laptop.Ad.Seller;

                //For images we will iterate through Images from Database & see which Images belong to this Ad
                //then we will send this Images List to laptopAdViewMode.Images
                //-JUST LEAVE IT- bcz i think this will increase load times. 
                laptopAdViewModel.Images = laptop.Ad.Images;

                //now add this viewmodel in List
                laptopAdViewModels.Add(laptopAdViewModel);
            }

            //Now return this list of LaptopAdViewModel to View
            return View(laptopAdViewModels);

        }

        // GET: LaptopAds/Details/5
        public ActionResult Details(int? id)
        {
            //on this page user will be looking for details of a Laptop by sending an ID
            
            //first check if there is any ID given with this page 
            if (id == null)
            {
                //return Bad Request if LaptopAds/Details/ & No ID in URL
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);
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
            //Displaying the form for user to create Laptop Ad
            //We have to populate the Create form for those DropDown & User Details like Country, State & City in their already.

            //Create repositories for Dataacess & send them with the View
            IRepositoryBase<Seller> Sellers = new SellersRepository(myDataContext);
            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(myDataContext);
            IRepositoryBase<Country> Countries = new CountriesRepository(myDataContext);
            IRepositoryBase<State> States = new StatesRepository(myDataContext);
            IRepositoryBase<City> Cities = new CitiesRepository(myDataContext);
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(myDataContext);

            //First get Current User details so we can know about Seller
            string CurrentUserName = User.Identity.GetUserName();

            Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

            //Create a Model & populate then send it to the View
            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();
            laptopAdViewModel.CountryID = seller.CountryID;
            laptopAdViewModel.StateID = seller.StateID;
            laptopAdViewModel.CityID = seller.CityID;

            laptopAdViewModel.Country = seller.Country;
            laptopAdViewModel.State = seller.State;
            laptopAdViewModel.City = seller.City;

            Currency currency = Currencies.GetAll().Where(c => c.CountryID == seller.CountryID).FirstOrDefault();
            laptopAdViewModel.Currency = currency;

            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name");
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType");
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name");
            ViewBag.StateID = new SelectList(States.GetAll().Where(c=>c.CountryID == seller.CountryID), "ID", "Name");
            ViewBag.CityID = new SelectList(Cities.GetAll().Where(c=>c.StateID == seller.StateID), "ID", "Name");
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name", currency.ID);

            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,Ram,Processor,HardDisk,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID")] LaptopAdViewModel laptopAdViewModel, HttpPostedFileBase ImageFile)
        {
            //User has submitted the Create Form with LaptopAdViewModel details & maybe ImageFile
            //We have to get the details from LaptopAdViewModel into Ad, Laptop & Image Models

            //Create Repositories for DataAccess
            IRepositoryBase<Ad> Ads = new AdsRepository(myDataContext);
            IRepositoryBase<Seller> Sellers = new SellersRepository(myDataContext);
            IRepositoryBase<Image> Images = new ImagesRepository(myDataContext);
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);
            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(myDataContext);
            IRepositoryBase<Country> Countries = new CountriesRepository(myDataContext);
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(myDataContext);

            if (ModelState.IsValid)
            {
                //New Ad Object to be populated & then Added into Repository
                Ad ad = new Ad();

                //populate this Ad Object
                ad.Title = laptopAdViewModel.Title;
                ad.ConditionID = laptopAdViewModel.ConditionID;
                ad.Description = laptopAdViewModel.Description;
                ad.CurrencyID = laptopAdViewModel.CurrencyID;
                ad.Price = laptopAdViewModel.Price;
                ad.CountryID = laptopAdViewModel.CountryID;
                ad.StateID = laptopAdViewModel.StateID;
                ad.CityID = laptopAdViewModel.CityID;

                //Get Current User Details to be send as Seller Details in Ad Object
                string CurrentUserName = User.Identity.GetUserName();
                ad.SellerID = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                ad.Slug = laptopAdViewModel.Title.Replace(' ', '-');
                ad.PostingTime = DateTime.Now;

                //Now Add this ad object in our Ads Repository
                Ads.Insert(ad);
                Ads.Commit();
                
                //Now Create the Laptop Object which we want to store in our database
                Laptop laptop = new Laptop();

                //populate this object too from LaptopAdViewModel
                laptop.AccessoryBrandID = laptopAdViewModel.AccessoryBrandID;
                laptop.OperatingSystem = laptopAdViewModel.OperatingSystem;
                laptop.Ram = laptopAdViewModel.Ram;
                laptop.Processor = laptopAdViewModel.Processor;
                laptop.HardDisk = laptopAdViewModel.HardDisk;
                laptop.AdID = ad.ID;

                //Add this new laptop details to Database
                Laptops.Insert(laptop);
                Laptops.Commit();

                //Now Check if user has submitted a legal Image file
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    //We have to store this image in our Database as Image Path & then save it in our Images table with AdID

                    //Get Image Path
                    var uploadDir = "~/images";
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    //Save this image in our images folder
                    ImageFile.SaveAs(ImagePath);

                    //Create Image object & add AdID from ad object
                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    //Insert this Image object in Database
                    Images.Insert(image);
                    Images.Commit();
                }

                //If Ad is added to Database we goto LaptopAds/Index page to view all ads.
                //we can change it to show us the page of our add too - Recomended
                return RedirectToAction("Index");
            }

            //if ModelState is not in good & Valid we come here
            
            //We will again populate our ViewBag to be send back to user so he dont have to write everything again 
            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name");
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType");
            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name");
            ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name");
            
            //return the same LaptopAdViewModel back
            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);
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

            IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            IRepositoryBase<Condition> Conditions = new ConditionsRepository(myDataContext);
            IRepositoryBase<Country> Countries = new CountriesRepository(myDataContext);
            IRepositoryBase<State> States = new StatesRepository(myDataContext);
            IRepositoryBase<City> Cities = new CitiesRepository(myDataContext);
            IRepositoryBase<Currency> Currencies = new CurrenciesRepository(myDataContext);

            ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name", laptop.AccessoryBrandID);
            ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType", laptop.Ad.ConditionID);

            ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name", laptop.Ad.CountryID);
            ViewBag.StateID = new SelectList(States.GetAll().Where(c => c.CountryID == laptop.Ad.CountryID), "ID", "Name", laptop.Ad.StateID);
            ViewBag.CityID = new SelectList(Cities.GetAll().Where(c => c.StateID == laptop.Ad.StateID), "ID", "Name", laptop.Ad.CityID);

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
                IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);

                Laptop laptop = Laptops.GetByID(laptopAdViewModel.ID);
                
                laptop.AccessoryBrandID = laptopAdViewModel.AccessoryBrandID;
                laptop.OperatingSystem = laptopAdViewModel.OperatingSystem;
                laptop.Ram = laptopAdViewModel.Ram;
                laptop.Processor = laptopAdViewModel.Processor;
                laptop.HardDisk = laptopAdViewModel.HardDisk;

                Laptops.Update(laptop);
                Laptops.Commit();

                IRepositoryBase<Ad> Ads = new AdsRepository(myDataContext);

                Ad ad = Ads.GetByID(laptop.AdID);

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    //if the user uploaded a new image we have to do 2 things
                    //1- Delete the previous image record from image table & images folder
                    //2- Save the new image & its record in images table

                    IRepositoryBase<Image> Images = new ImagesRepository(myDataContext);

                    var uploadDir = "~/images";

                    if (ad.Images != null && ad.Images.Count >0)
                    {
                        Image oldimage = ad.Images.First();

                        //1- Delete the previous record
                        Images.Delete(oldimage);
                        Images.Commit();

                        //Now delete the previous image from images directory
                        var OldImagePath = Path.Combine(Server.MapPath(uploadDir), oldimage.Path);

                        System.IO.File.Delete(OldImagePath);
                    }

                    //2 - Lets update the new image
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    ImageFile.SaveAs(ImagePath);


                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    Images.Insert(image);
                    Images.Commit();
                }

                ad.Title = laptopAdViewModel.Title;
                ad.ConditionID = laptopAdViewModel.ConditionID;
                ad.Description = laptopAdViewModel.Description;
                ad.CurrencyID = laptopAdViewModel.CurrencyID;
                ad.Price = laptopAdViewModel.Price;
                ad.CountryID = laptopAdViewModel.CountryID;
                ad.StateID = laptopAdViewModel.StateID;
                ad.CityID = laptopAdViewModel.CityID;

                ad.Slug = laptopAdViewModel.Title.Replace(' ', '-');

                Ads.Update(ad);

                Ads.Commit();

                return RedirectToAction("Index");
            }

            //ViewBag.AccessoryBrandID = new SelectList(db.AccessoryBrands, "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            //ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", laptopAdViewModel.CityID);
            //ViewBag.ConditionID = new SelectList(db.Conditions, "ID", "ConditionType", laptopAdViewModel.ConditionID);
            //ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", laptopAdViewModel.CountryID);
            //ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", laptopAdViewModel.CurrencyID);
            //ViewBag.StateID = new SelectList(db.States, "ID", "Name", laptopAdViewModel.StateID);


            //IRepositoryBase<AccessoryBrand> AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            //IRepositoryBase<Condition> Conditions = new ConditionsRepository(myDataContext);
            //IRepositoryBase<Country> Countries = new CountriesRepository(myDataContext);
            //IRepositoryBase<Currency> Currencies = new CurrenciesRepository(myDataContext);


            //ViewBag.AccessoryBrandID = new SelectList(AccessoryBrands.GetAll(), "ID", "Name", laptopAdViewModel.AccessoryBrandID);
            //ViewBag.ConditionID = new SelectList(Conditions.GetAll(), "ID", "ConditionType", laptopAdViewModel.ConditionID);
            //ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name", laptopAdViewModel.CountryID);
            //ViewBag.CurrencyID = new SelectList(Currencies.GetAll(), "ID", "Name", laptopAdViewModel.CurrencyID);


            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);
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

            IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);
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
                IRepositoryBase<Laptop> Laptops = new LaptopsRepository(myDataContext);

                Laptops.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
