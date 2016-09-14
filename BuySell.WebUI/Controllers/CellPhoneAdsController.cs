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
using Microsoft.AspNet.Identity;
using System.IO;

namespace BuySell.WebUI.Controllers
{
    public class CellPhoneAdsController : Controller
    {
        private DataContext myDataContext = new DataContext();

        //Create Repositories for DataAccess
        IRepositoryBase<Seller> Sellers;
        IRepositoryBase<Ad> Ads;
        IRepositoryBase<CellPhone> CellPhones;
        IRepositoryBase<AccessoryBrand> AccessoryBrands;
        IRepositoryBase<Condition> Conditions;
        IRepositoryBase<Currency> Currencies;
        IRepositoryBase<Country> Countries;
        IRepositoryBase<State> States;
        IRepositoryBase<City> Cities;
        IRepositoryBase<Image> Images;
        IRepositoryBase<Review> Reviews;

        public CellPhoneAdsController()
        {
            //Create Repositories for DataAccess
            Ads = new AdsRepository(myDataContext);
            Sellers = new SellersRepository(myDataContext);
            Images = new ImagesRepository(myDataContext);
            CellPhones = new CellPhonesRepository(myDataContext);
            AccessoryBrands = new AccessoryBrandsRepository(myDataContext);
            Conditions = new ConditionsRepository(myDataContext);
            Currencies = new CurrenciesRepository(myDataContext);
            Countries = new CountriesRepository(myDataContext);
            States = new StatesRepository(myDataContext);
            Cities = new CitiesRepository(myDataContext);
            Reviews = new ReviewsRepository(myDataContext);
        }

        // GET: CellPhonesAds
        public ActionResult Index()
        {
            List<CellPhone> CellPhonesList = CellPhones.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(12).ToList();

            List<CellPhoneAdViewModel> cellPhoneAdViewModels = new List<CellPhoneAdViewModel>();

            foreach (CellPhone cellPhone in CellPhonesList)
            {
                CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

                cellPhoneAdViewModel.ID = cellPhone.ID;
                cellPhoneAdViewModel.Title = cellPhone.Ad.Title;
                cellPhoneAdViewModel.AccessoryBrandID = cellPhone.AccessoryBrandID;
                cellPhoneAdViewModel.AccessoryBrand = cellPhone.AccessoryBrand;
                cellPhoneAdViewModel.OperatingSystem = cellPhone.OperatingSystem;
                cellPhoneAdViewModel.Condition = cellPhone.Ad.Condition;
                cellPhoneAdViewModel.ConditionID = cellPhone.Ad.ConditionID;
                cellPhoneAdViewModel.Description = cellPhone.Ad.Description;
                cellPhoneAdViewModel.Currency = cellPhone.Ad.Currency;
                cellPhoneAdViewModel.CurrencyID = cellPhone.Ad.CurrencyID;
                cellPhoneAdViewModel.Price = cellPhone.Ad.Price;
                cellPhoneAdViewModel.Country = cellPhone.Ad.Country;
                cellPhoneAdViewModel.CountryID = cellPhone.Ad.CountryID;
                cellPhoneAdViewModel.StateID = cellPhone.Ad.StateID;
                cellPhoneAdViewModel.State = cellPhone.Ad.State;
                cellPhoneAdViewModel.CityID = cellPhone.Ad.CityID;
                cellPhoneAdViewModel.City = cellPhone.Ad.City;
                cellPhoneAdViewModel.SellerID = cellPhone.Ad.SellerID;
                cellPhoneAdViewModel.Seller = cellPhone.Ad.Seller;

                cellPhoneAdViewModel.Images = cellPhone.Ad.Images;

                cellPhoneAdViewModel.Reviews = cellPhone.Ad.Reviews;

                cellPhoneAdViewModels.Add(cellPhoneAdViewModel);
            }

            return View(cellPhoneAdViewModels);
        }

        // GET: CellPhonesAds/Details/5
        public ActionResult Details(int? id)
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
            CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

            cellPhoneAdViewModel.ID = cellPhone.ID;
            cellPhoneAdViewModel.Title = cellPhone.Ad.Title;
            cellPhoneAdViewModel.AccessoryBrand = cellPhone.AccessoryBrand;
            cellPhoneAdViewModel.AccessoryBrandID = cellPhone.AccessoryBrandID;
            cellPhoneAdViewModel.OperatingSystem = cellPhone.OperatingSystem;
            cellPhoneAdViewModel.Condition = cellPhone.Ad.Condition;
            cellPhoneAdViewModel.ConditionID = cellPhone.Ad.ConditionID;
            cellPhoneAdViewModel.Description = cellPhone.Ad.Description;
            cellPhoneAdViewModel.Currency = cellPhone.Ad.Currency;
            cellPhoneAdViewModel.CurrencyID = cellPhone.Ad.CurrencyID;
            cellPhoneAdViewModel.Price = cellPhone.Ad.Price;
            cellPhoneAdViewModel.Country = cellPhone.Ad.Country;
            cellPhoneAdViewModel.CountryID = cellPhone.Ad.CountryID;
            cellPhoneAdViewModel.State = cellPhone.Ad.State;
            cellPhoneAdViewModel.StateID = cellPhone.Ad.StateID;
            cellPhoneAdViewModel.City = cellPhone.Ad.City;
            cellPhoneAdViewModel.CityID = cellPhone.Ad.CityID;
            cellPhoneAdViewModel.Seller = cellPhone.Ad.Seller;
            cellPhoneAdViewModel.SellerID = cellPhone.Ad.SellerID;
            cellPhoneAdViewModel.PostingTime = cellPhone.Ad.PostingTime;

            //now for Image we will go back to Images Repository & match AdID there.
            cellPhoneAdViewModel.Images = Images.GetAll().Where(i => i.AdID == cellPhone.AdID).ToList();

            //now for Reviews
            cellPhoneAdViewModel.Reviews = Reviews.GetAll().Where(i => i.AdID == cellPhone.AdID).ToList();

            return View(cellPhoneAdViewModel);
        }

        // GET: CellPhonesAds/Create
        [Authorize]
        public ActionResult Create()
        {
            string CurrentUserName = User.Identity.GetUserName();
            Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

            CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

            cellPhoneAdViewModel.CountryID = seller.CountryID;
            cellPhoneAdViewModel.Country = seller.Country;
            cellPhoneAdViewModel.State = seller.State;
            cellPhoneAdViewModel.StateID = seller.StateID;
            cellPhoneAdViewModel.City = seller.City;
            cellPhoneAdViewModel.CityID = seller.CityID;
            cellPhoneAdViewModel.Seller = seller;
            cellPhoneAdViewModel.SellerID = seller.ID;

            Currency currency = Currencies.GetAll().Where(c => c.CountryID == seller.CountryID).FirstOrDefault();

            if (currency != null)
            {
                cellPhoneAdViewModel.CurrencyID = currency.ID;
                cellPhoneAdViewModel.Currency = currency;
            }

            //Populate Lists in ViewModel to be shown on View
            cellPhoneAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            cellPhoneAdViewModel.ConditionsList = Conditions.GetAll();
            cellPhoneAdViewModel.CurrenciesList = Currencies.GetAll();
            cellPhoneAdViewModel.CountriesList = Countries.GetAll();
            cellPhoneAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == seller.CountryID);
            cellPhoneAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == seller.StateID);

            return View(cellPhoneAdViewModel);
        }

        // POST: CellPhonesAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID,SellerID,PostingTime")] CellPhoneAdViewModel cellPhoneAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                Ad ad = new Ad();

                ad.Title = cellPhoneAdViewModel.Title;
                ad.ConditionID = cellPhoneAdViewModel.ConditionID;
                ad.Description = cellPhoneAdViewModel.Description;
                ad.CurrencyID = cellPhoneAdViewModel.CurrencyID;
                ad.Price = cellPhoneAdViewModel.Price;
                ad.CountryID = cellPhoneAdViewModel.CountryID;
                ad.StateID = cellPhoneAdViewModel.StateID;
                ad.CityID = cellPhoneAdViewModel.CityID;

                string CurrentUserName = User.Identity.GetUserName();
                ad.SellerID = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                ad.Slug = cellPhoneAdViewModel.Title.Replace(' ', '-');
                ad.PostingTime = DateTime.Now;

                Ads.Insert(ad);
                Ads.Commit();

                CellPhone cellPhone = new CellPhone();

                cellPhone.AccessoryBrandID = cellPhoneAdViewModel.AccessoryBrandID;
                cellPhone.OperatingSystem = cellPhoneAdViewModel.OperatingSystem;
                cellPhone.AdID = ad.ID;

                CellPhones.Insert(cellPhone);
                CellPhones.Commit();

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadDir = "~/images";
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    ImageFile.SaveAs(ImagePath);

                    var image = new Image
                    {
                        Path = NewImageName,
                        AdID = ad.ID
                    };

                    Images.Insert(image);
                    Images.Commit();
                }

                return RedirectToAction("Details", "CellPhoneAds", new { id = cellPhone.ID });
            }

            cellPhoneAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            cellPhoneAdViewModel.ConditionsList = Conditions.GetAll();
            cellPhoneAdViewModel.CurrenciesList = Currencies.GetAll();
            cellPhoneAdViewModel.CountriesList = Countries.GetAll();
            cellPhoneAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == cellPhoneAdViewModel.CountryID);
            cellPhoneAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == cellPhoneAdViewModel.StateID);


            return View(cellPhoneAdViewModel);
        }

        // GET: CellPhonesAds/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
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
            CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

            cellPhoneAdViewModel.ID = cellPhone.ID;
            cellPhoneAdViewModel.Title = cellPhone.Ad.Title;
            cellPhoneAdViewModel.AccessoryBrand = cellPhone.AccessoryBrand;
            cellPhoneAdViewModel.AccessoryBrandID = cellPhone.AccessoryBrandID;
            cellPhoneAdViewModel.OperatingSystem = cellPhone.OperatingSystem;
            cellPhoneAdViewModel.Condition = cellPhone.Ad.Condition;
            cellPhoneAdViewModel.ConditionID = cellPhone.Ad.ConditionID;
            cellPhoneAdViewModel.Description = cellPhone.Ad.Description;
            cellPhoneAdViewModel.Currency = cellPhone.Ad.Currency;
            cellPhoneAdViewModel.CurrencyID = cellPhone.Ad.CurrencyID;
            cellPhoneAdViewModel.Price = cellPhone.Ad.Price;
            cellPhoneAdViewModel.Country = cellPhone.Ad.Country;
            cellPhoneAdViewModel.CountryID = cellPhone.Ad.CountryID;
            cellPhoneAdViewModel.State = cellPhone.Ad.State;
            cellPhoneAdViewModel.StateID = cellPhone.Ad.StateID;
            cellPhoneAdViewModel.City = cellPhone.Ad.City;
            cellPhoneAdViewModel.CityID = cellPhone.Ad.CityID;

            cellPhoneAdViewModel.AccessoryBrandsList = AccessoryBrands.GetAll();
            cellPhoneAdViewModel.ConditionsList = Conditions.GetAll();
            cellPhoneAdViewModel.CurrenciesList = Currencies.GetAll();
            cellPhoneAdViewModel.CountriesList = Countries.GetAll();
            cellPhoneAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == cellPhone.Ad.CountryID);
            cellPhoneAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == cellPhone.Ad.StateID);

            return View(cellPhoneAdViewModel);
        }

        // POST: CellPhonesAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,AccessoryBrandID,OperatingSystem,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID,SellerID,PostingTime,Review")] CellPhoneAdViewModel cellPhoneAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                CellPhone cellPhone = CellPhones.GetByID(cellPhoneAdViewModel.ID);

                cellPhone.AccessoryBrandID = cellPhoneAdViewModel.AccessoryBrandID;
                cellPhone.AccessoryBrand = cellPhoneAdViewModel.AccessoryBrand; // <------------------------This might give error bcz the view may send a model that may not have this AccessoryBrand
                cellPhone.OperatingSystem = cellPhoneAdViewModel.OperatingSystem;

                CellPhones.Update(cellPhone);
                CellPhones.Commit();

                Ad ad = Ads.GetByID(cellPhone.AdID);

                ad.Title = cellPhoneAdViewModel.Title;
                ad.ConditionID = cellPhoneAdViewModel.ConditionID;
                ad.Condition = cellPhoneAdViewModel.Condition;
                ad.Description = cellPhoneAdViewModel.Description;
                ad.CurrencyID = cellPhoneAdViewModel.CurrencyID;
                ad.Currency = cellPhoneAdViewModel.Currency;
                ad.Price = cellPhoneAdViewModel.Price;
                ad.CountryID = cellPhoneAdViewModel.CountryID;
                ad.Country = cellPhoneAdViewModel.Country;
                ad.StateID = cellPhoneAdViewModel.StateID;
                ad.State = cellPhoneAdViewModel.State;
                ad.CityID = cellPhoneAdViewModel.CityID;
                ad.City = cellPhoneAdViewModel.City;
                ad.Slug = cellPhoneAdViewModel.Title.Replace(' ', '-');

                Ads.Update(ad);
                Ads.Commit();

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var uploadDir = "~/images";

                    if (ad.Images != null && ad.Images.Count > 0)
                    {
                        Image oldimage = ad.Images.First();

                        Images.Delete(oldimage);
                        Images.Commit();

                        var OldImagePath = Path.Combine(Server.MapPath(uploadDir), oldimage.Path);

                        System.IO.File.Delete(OldImagePath);
                    }

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

                return RedirectToAction("Details", "CellPhoneAds", new { id = cellPhone.ID });
            }

            return View(cellPhoneAdViewModel);
        }

        // GET: CellPhonesAds/Delete/5
        [Authorize]
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

            CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

            cellPhoneAdViewModel.ID = cellPhone.ID;
            cellPhoneAdViewModel.Title = cellPhone.Ad.Title;
            cellPhoneAdViewModel.AccessoryBrandID = cellPhone.AccessoryBrandID;
            cellPhoneAdViewModel.AccessoryBrand = cellPhone.AccessoryBrand;
            cellPhoneAdViewModel.OperatingSystem = cellPhone.OperatingSystem;
            cellPhoneAdViewModel.ConditionID = cellPhone.Ad.ConditionID;
            cellPhoneAdViewModel.Condition = cellPhone.Ad.Condition;
            cellPhoneAdViewModel.Description = cellPhone.Ad.Description;
            cellPhoneAdViewModel.CurrencyID = cellPhone.Ad.CurrencyID;
            cellPhoneAdViewModel.Currency = cellPhone.Ad.Currency;
            cellPhoneAdViewModel.Price = cellPhone.Ad.Price;
            cellPhoneAdViewModel.CountryID = cellPhone.Ad.CountryID;
            cellPhoneAdViewModel.Country = cellPhone.Ad.Country;
            cellPhoneAdViewModel.StateID = cellPhone.Ad.StateID;
            cellPhoneAdViewModel.State = cellPhone.Ad.State;
            cellPhoneAdViewModel.CityID = cellPhone.Ad.CityID;
            cellPhoneAdViewModel.City = cellPhone.Ad.City;
            cellPhoneAdViewModel.SellerID = cellPhone.Ad.SellerID;
            cellPhoneAdViewModel.Seller = cellPhone.Ad.Seller;

            return View(cellPhoneAdViewModel);
        }

        // POST: CellPhonesAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            CellPhone cellPhone = CellPhones.GetByID(id);

            CellPhones.Delete(cellPhone);
            CellPhones.Commit();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview(CellPhoneAdViewModel cellPhoneAdViewModel, int? id, string Review)
        {
            if (Review.Length >= 50)
            {
                CellPhone cellPhone = CellPhones.GetByID(id);

                if (cellPhone == null)
                {
                    return HttpNotFound();
                }

                string CurrentUserName = User.Identity.GetUserName();

                Review newReview = new Review();

                newReview.Content = Review;
                newReview.AdID = cellPhone.AdID;
                newReview.Ad = cellPhone.Ad;
                newReview.PostingTime = DateTime.Now;

                Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

                newReview.SellerID = seller.ID;
                newReview.Seller = seller;

                newReview.ReviewStars = 5;

                Reviews.Insert(newReview);
                Reviews.Commit();

                return RedirectToAction("Details", "CellPhoneAds", new { id = id });
            }
            return RedirectToAction("Details", "CellPhoneAds", new { id = id });
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
