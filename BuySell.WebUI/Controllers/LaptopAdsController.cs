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
        //Main DataContext object to be sent to Repository
        DataContext myDataContext = new DataContext();

        //Create Repositories for DataAccess
        IRepositoryBase<Seller> Sellers;
        IRepositoryBase<Ad> Ads;
        IRepositoryBase<Laptop> Laptops;
        IRepositoryBase<AccessoryBrand> AccessoryBrands;
        IRepositoryBase<Condition> Conditions;
        IRepositoryBase<Currency> Currencies;
        IRepositoryBase<Country> Countries;
        IRepositoryBase<State> States;
        IRepositoryBase<City> Cities;
        IRepositoryBase<Image> Images;
        IRepositoryBase<Review> Reviews;

        public LaptopAdsController()
        {
            //Create Repositories for DataAccess
            Ads = new AdsRepository(myDataContext);
            Sellers = new SellersRepository(myDataContext);
            Images = new ImagesRepository(myDataContext);
            Laptops = new LaptopsRepository(myDataContext);
            AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            Conditions = new ConditionsRepository(myDataContext);
            Currencies = new CurrenciesRepository(myDataContext);
            Countries = new CountriesRepository(myDataContext);
            States = new StatesRepository(myDataContext);
            Cities = new CitiesRepository(myDataContext);
            Reviews = new ReviewsRepository(myDataContext);
        }

        // GET: LaptopAds
        public ActionResult Index()
        {
            //This is the main page for Laptop Ads.
            //we will get records from Database into this page.

            //Get All Laptops as List
            //Aditionally I only want to get last 12 Laptop Ads in Descending Order so as not to bombard the Index Page
            List<Laptop> LaptopsList = Laptops.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(12).ToList();

            //There will be many Laptop Ads so we created a whole List of LaptopAdViewModel
            List<LaptopAdViewModel> laptopAdViewModels = new List<LaptopAdViewModel>();

            //Now Iterate over this List of laptops to populate LaptopAdVieqModels List
            foreach (Laptop laptop in LaptopsList)
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

                //Get the reviews from Laptops Ads Review relation
                laptopAdViewModel.Reviews = laptop.Ad.Reviews;

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

            //Get the Laptop by Given ID
            Laptop laptop = Laptops.GetByID(id);

            //Check if Laptop Exists
            if (laptop == null)
            {
                //if not exists then Show 404 Not found
                return HttpNotFound();
            }

            //if laptop is found then Populate the LaptopAdViewModel
            //create an object
            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            //Populate it from Laptop Details
            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
            laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
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
            laptopAdViewModel.State = laptop.Ad.State;
            laptopAdViewModel.StateID = laptop.Ad.StateID;
            laptopAdViewModel.City = laptop.Ad.City;
            laptopAdViewModel.CityID = laptop.Ad.CityID;
            laptopAdViewModel.Seller = laptop.Ad.Seller;
            laptopAdViewModel.SellerID = laptop.Ad.SellerID;
            laptopAdViewModel.PostingTime = laptop.Ad.PostingTime;

            //now for Image we will go back to Images Repository & match AdID there.
            laptopAdViewModel.Images = Images.GetAll().Where(i=>i.AdID==laptop.AdID).ToList();

            //now for Reviews
            laptopAdViewModel.Reviews = Reviews.GetAll().Where(i => i.AdID == laptop.AdID).ToList();

            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Create
        [Authorize]
        public ActionResult Create()
        {
            //Displaying the form for user to create Laptop Ad
            //We have to populate the Create form for those DropDown & User Details like Country, State & City in their already.

            //First get Current User details so we can know about Seller
            string CurrentUserName = User.Identity.GetUserName();
            Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

            //Create a Model & populate then send it to the View
            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();
            laptopAdViewModel.CountryID = seller.CountryID;
            laptopAdViewModel.Country = seller.Country;
            laptopAdViewModel.State = seller.State;
            laptopAdViewModel.StateID = seller.StateID;
            laptopAdViewModel.City = seller.City;
            laptopAdViewModel.CityID = seller.CityID;
            laptopAdViewModel.Seller = seller;
            laptopAdViewModel.SellerID = seller.ID;

            Currency currency = Currencies.GetAll().Where(c => c.CountryID == seller.CountryID).FirstOrDefault();

            if (currency != null)
            {
                laptopAdViewModel.CurrencyID = currency.ID;
                laptopAdViewModel.Currency = currency;
            }

            //Populate Lists in ViewModel to be shown on View
            laptopAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            laptopAdViewModel.ConditionsList = Conditions.GetAll();
            laptopAdViewModel.CurrenciesList = Currencies.GetAll();
            laptopAdViewModel.CountriesList = Countries.GetAll();
            laptopAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == seller.CountryID);
            laptopAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == seller.StateID);

            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,Ram,Processor,HardDisk,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID")] LaptopAdViewModel laptopAdViewModel, HttpPostedFileBase ImageFile)
        {
            //User has submitted the Create Form with LaptopAdViewModel details & maybe ImageFile
            //We have to get the details from LaptopAdViewModel into Ad, Laptop & Image Models

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
                return RedirectToAction("Details", "LaptopAds", new { id = laptop.ID });
            }

            //if ModelState is not in good & Valid we come here

            //Populate Lists in ViewModel to be shown on View
            laptopAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            laptopAdViewModel.ConditionsList = Conditions.GetAll();
            laptopAdViewModel.CurrenciesList = Currencies.GetAll();
            laptopAdViewModel.CountriesList = Countries.GetAll();
            laptopAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == laptopAdViewModel.CountryID);
            laptopAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == laptopAdViewModel.StateID);

            //return the same LaptopAdViewModel back
            return View(laptopAdViewModel);
        }

        // GET: LaptopAds/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
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
            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
            laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
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
            laptopAdViewModel.State = laptop.Ad.State;
            laptopAdViewModel.StateID = laptop.Ad.StateID;
            laptopAdViewModel.City = laptop.Ad.City;
            laptopAdViewModel.CityID = laptop.Ad.CityID;

            //Populate Lists in ViewModel to be shown on View
            laptopAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            laptopAdViewModel.ConditionsList = Conditions.GetAll();
            laptopAdViewModel.CurrenciesList = Currencies.GetAll();
            laptopAdViewModel.CountriesList = Countries.GetAll();
            laptopAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == laptop.Ad.CountryID);
            laptopAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == laptop.Ad.StateID);
            
            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,Ram,Processor,HardDisk,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID")] LaptopAdViewModel laptopAdViewModel, HttpPostedFileBase ImageFile)
        {
            //Now user has submitted the Edit page with his new details 
            if (ModelState.IsValid)
            {
                //since this laptop already exists we will get it by the ID from LaptopAdViewModel that we got in parameter
                Laptop laptop = Laptops.GetByID(laptopAdViewModel.ID);

                //now update this laptop fields from the Model
                laptop.AccessoryBrandID = laptopAdViewModel.AccessoryBrandID;
                laptop.AccessoryBrand = laptopAdViewModel.AccessoryBrand; // <------------------------This might give error bcz the view may send a model that may not have this AccessoryBrand
                laptop.OperatingSystem = laptopAdViewModel.OperatingSystem;
                laptop.Ram = laptopAdViewModel.Ram;
                laptop.Processor = laptopAdViewModel.Processor;
                laptop.HardDisk = laptopAdViewModel.HardDisk;

                //Update this laptop in Database
                Laptops.Update(laptop);
                Laptops.Commit();

                //Now if the user updated any Ad fields
                //Get an Ad object from Databse since it already exists there.
                Ad ad = Ads.GetByID(laptop.AdID);

                //update this object from the Model we got
                ad.Title = laptopAdViewModel.Title;
                ad.ConditionID = laptopAdViewModel.ConditionID;
                ad.Condition = laptopAdViewModel.Condition;
                ad.Description = laptopAdViewModel.Description;
                ad.CurrencyID = laptopAdViewModel.CurrencyID;
                ad.Currency = laptopAdViewModel.Currency;
                ad.Price = laptopAdViewModel.Price;
                ad.CountryID = laptopAdViewModel.CountryID;
                ad.Country = laptopAdViewModel.Country;
                ad.StateID = laptopAdViewModel.StateID;
                ad.State = laptopAdViewModel.State;
                ad.CityID = laptopAdViewModel.CityID;
                ad.City = laptopAdViewModel.City;
                ad.Slug = laptopAdViewModel.Title.Replace(' ', '-');

                //Now update this object in database
                Ads.Update(ad);
                Ads.Commit();

                //Now if there is new updated image with form, 
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    //we have to do 2 things here
                    //1- Delete the previous image record from image table & images folder
                    //2- Save the new image & its record in images table

                    var uploadDir = "~/images";

                    //check if the ad have any images
                    if (ad.Images != null && ad.Images.Count > 0)
                    {
                        //get the first image from this ad
                        //i am not getting a list here bcz the delete method that i have do not get a list
                        //Yes I think i can loop through the list if i get & dlete one by one --- but choro yar. esy he theek hy.
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

                    //Upload the image in our images diretory
                    ImageFile.SaveAs(ImagePath);

                    //create new Image object
                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    //add this image object in Database
                    Images.Insert(image);
                    Images.Commit();
                }

                //get user back to the details page of this LaptopAds
                return RedirectToAction("Details", "LaptopAds", new { id = laptop.ID });
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
        [Authorize]
        public ActionResult Delete(int? id)
        {
            //User is trying to delete this object

            //check if the ID supplied is not null
            if (id == null)
            {
                //return bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get te laptop from database by the supplied ID
            Laptop laptop = Laptops.GetByID(id);

            //see if this laptop exists
            if (laptop == null)
            {
                //if not then 404 Not Found
                return HttpNotFound();
            }

            //since the laptop exists now we populate the view model from it
            LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

            laptopAdViewModel.ID = laptop.ID;
            laptopAdViewModel.Title = laptop.Ad.Title;
            laptopAdViewModel.AccessoryBrandID = laptop.AccessoryBrandID;
            laptopAdViewModel.AccessoryBrand = laptop.AccessoryBrand;
            laptopAdViewModel.OperatingSystem = laptop.OperatingSystem;
            laptopAdViewModel.Ram = laptop.Ram;
            laptopAdViewModel.Processor = laptop.Processor;
            laptopAdViewModel.HardDisk = laptop.HardDisk;
            laptopAdViewModel.ConditionID = laptop.Ad.ConditionID;
            laptopAdViewModel.Condition = laptop.Ad.Condition;
            laptopAdViewModel.Description = laptop.Ad.Description;
            laptopAdViewModel.CurrencyID = laptop.Ad.CurrencyID;
            laptopAdViewModel.Currency = laptop.Ad.Currency;
            laptopAdViewModel.Price = laptop.Ad.Price;
            laptopAdViewModel.CountryID = laptop.Ad.CountryID;
            laptopAdViewModel.Country = laptop.Ad.Country;
            laptopAdViewModel.StateID = laptop.Ad.StateID;
            laptopAdViewModel.State = laptop.Ad.State;
            laptopAdViewModel.CityID = laptop.Ad.CityID;
            laptopAdViewModel.City = laptop.Ad.City;
            laptopAdViewModel.SellerID = laptop.Ad.SellerID;
            laptopAdViewModel.Seller = laptop.Ad.Seller;

            //send this model to User for confirmation to delete
            //i think we should not have this view altogether. why show this whole form
            //we should show user a jquery confirmation & then we delete on the basis of result from there.
            return View(laptopAdViewModel);
        }

        // POST: LaptopAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            //user has confirmed to delete a record. Delete it right now.

            //get the laptop by supplied ID
            Laptop laptop = Laptops.GetByID(id);

            //delete from database
            Laptops.Delete(laptop);
            Laptops.Commit();

            //return to main LaptopAds page
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview(LaptopAdViewModel laptopAdViewModel, int? id, string Review)
        {
            if (Review.Length >= 50)
            {
                //Get the Ad Object from the ID by Laptop Object
                Laptop laptop = Laptops.GetByID(id);

                if (laptop == null)
                {
                    return HttpNotFound();
                }

                //Get Current User Details to be send as Seller Details in Review Object
                string CurrentUserName = User.Identity.GetUserName();

                Review newReview = new Review();

                newReview.Content = Review;
                newReview.AdID = laptop.AdID;
                newReview.Ad = laptop.Ad;
                newReview.PostingTime = DateTime.Now;

                Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

                newReview.SellerID = seller.ID;
                newReview.Seller = seller;

                newReview.ReviewStars = 5; //for now I dont want to implement this functionality but its good to keep for later.

                Reviews.Insert(newReview);
                Reviews.Commit();

                //Now return the user back to the details of this ad
                return RedirectToAction("Details", "LaptopAds", new { id = id });
            }
            
            //review was not correct. return back to URL
            return RedirectToAction("Details", "LaptopAds", new { id = id });
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
