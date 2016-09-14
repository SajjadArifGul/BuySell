
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
    public class BikeAdsController : Controller
    {
        private DataContext myDataContext = new DataContext();

        //Create Repositories for DataAccess
        IRepositoryBase<Seller> Sellers;
        IRepositoryBase<Ad> Ads;
        IRepositoryBase<Bike> Bikes;
        IRepositoryBase<VehicleBrand> VehicleBrands;
        IRepositoryBase<Condition> Conditions;
        IRepositoryBase<Currency> Currencies;
        IRepositoryBase<Country> Countries;
        IRepositoryBase<State> States;
        IRepositoryBase<City> Cities;
        IRepositoryBase<Image> Images;
        IRepositoryBase<Review> Reviews;
        IRepositoryBase<Year> Years;
        IRepositoryBase<Color> Colors;

        public BikeAdsController()
        {
            Ads = new AdsRepository(myDataContext);
            Sellers = new SellersRepository(myDataContext);
            Images = new ImagesRepository(myDataContext);
            Bikes = new BikesRepository(myDataContext);
            VehicleBrands = new VehicleBrandsRepository(myDataContext);
            Conditions = new ConditionsRepository(myDataContext);
            Currencies = new CurrenciesRepository(myDataContext);
            Countries = new CountriesRepository(myDataContext);
            States = new StatesRepository(myDataContext);
            Cities = new CitiesRepository(myDataContext);
            Reviews = new ReviewsRepository(myDataContext);
            Years = new YearsRepository(myDataContext);
            Colors = new ColorsRepository(myDataContext);

        }


        // GET: BikeAds
        public ActionResult Index()
        {
            List<Bike> BikesList = Bikes.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(12).ToList();

            List<BikeAdViewModel> bikeAdViewModels = new List<BikeAdViewModel>();

            foreach (Bike bike in BikesList)
            {
                BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();
                
                bikeAdViewModel.ID = bike.ID;
                bikeAdViewModel.Title = bike.Ad.Title;
                bikeAdViewModel.VehicleBrandID = bike.VehicleBrandID;
                bikeAdViewModel.VehicleBrand = bike.VehicleBrand;
                bikeAdViewModel.YearID = bike.YearID;
                bikeAdViewModel.Year = bike.Year;
                bikeAdViewModel.DrivenKilometers = bike.DrivenKilometers;
                bikeAdViewModel.ColorID = bike.ColorID;
                bikeAdViewModel.Color = bike.Color;
                bikeAdViewModel.Insurance = bike.Insurance;
                bikeAdViewModel.Condition = bike.Ad.Condition;
                bikeAdViewModel.ConditionID = bike.Ad.ConditionID;
                bikeAdViewModel.Description = bike.Ad.Description;
                bikeAdViewModel.Currency = bike.Ad.Currency;
                bikeAdViewModel.CurrencyID = bike.Ad.CurrencyID;
                bikeAdViewModel.Price = bike.Ad.Price;
                bikeAdViewModel.Country = bike.Ad.Country;
                bikeAdViewModel.CountryID = bike.Ad.CountryID;
                bikeAdViewModel.StateID = bike.Ad.StateID;
                bikeAdViewModel.State = bike.Ad.State;
                bikeAdViewModel.CityID = bike.Ad.CityID;
                bikeAdViewModel.City = bike.Ad.City;
                bikeAdViewModel.SellerID = bike.Ad.SellerID;
                bikeAdViewModel.Seller = bike.Ad.Seller;

                bikeAdViewModel.Images = bike.Ad.Images;

                bikeAdViewModel.Reviews = bike.Ad.Reviews;

                bikeAdViewModels.Add(bikeAdViewModel);
            }

            return View(bikeAdViewModels);
        }

        // GET: BikeAds/Details/5
        public ActionResult Details(int? id)
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
            BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();

            bikeAdViewModel.ID = bike.ID;
            bikeAdViewModel.Title = bike.Ad.Title;
            bikeAdViewModel.VehicleBrand = bike.VehicleBrand;
            bikeAdViewModel.VehicleBrandID = bike.VehicleBrandID;
            bikeAdViewModel.Year = bike.Year;
            bikeAdViewModel.YearID = bike.YearID;
            bikeAdViewModel.DrivenKilometers = bike.DrivenKilometers;
            bikeAdViewModel.Insurance = bike.Insurance;
            bikeAdViewModel.Color = bike.Color;
            bikeAdViewModel.ColorID = bike.ColorID;
            bikeAdViewModel.Condition = bike.Ad.Condition;
            bikeAdViewModel.ConditionID = bike.Ad.ConditionID;
            bikeAdViewModel.Description = bike.Ad.Description;
            bikeAdViewModel.Currency = bike.Ad.Currency;
            bikeAdViewModel.CurrencyID = bike.Ad.CurrencyID;
            bikeAdViewModel.Price = bike.Ad.Price;
            bikeAdViewModel.Country = bike.Ad.Country;
            bikeAdViewModel.CountryID = bike.Ad.CountryID;
            bikeAdViewModel.State = bike.Ad.State;
            bikeAdViewModel.StateID = bike.Ad.StateID;
            bikeAdViewModel.City = bike.Ad.City;
            bikeAdViewModel.CityID = bike.Ad.CityID;
            bikeAdViewModel.Seller = bike.Ad.Seller;
            bikeAdViewModel.SellerID = bike.Ad.SellerID;
            bikeAdViewModel.PostingTime = bike.Ad.PostingTime;

            bikeAdViewModel.Images = Images.GetAll().Where(i => i.AdID == bike.AdID).ToList();

            bikeAdViewModel.Reviews = Reviews.GetAll().Where(i => i.AdID == bike.AdID).ToList();

            return View(bikeAdViewModel);
        }

        // GET: BikeAds/Create
        [Authorize]
        public ActionResult Create()
        {
            string CurrentUserName = User.Identity.GetUserName();
            Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

            BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();

            bikeAdViewModel.CountryID = seller.CountryID;
            bikeAdViewModel.Country = seller.Country;
            bikeAdViewModel.State = seller.State;
            bikeAdViewModel.StateID = seller.StateID;
            bikeAdViewModel.City = seller.City;
            bikeAdViewModel.CityID = seller.CityID;
            bikeAdViewModel.Seller = seller;
            bikeAdViewModel.SellerID = seller.ID;

            Currency currency = Currencies.GetAll().Where(c => c.CountryID == seller.CountryID).FirstOrDefault();

            if (currency != null)
            {
                bikeAdViewModel.CurrencyID = currency.ID;
                bikeAdViewModel.Currency = currency;
            }

            bikeAdViewModel.VehicleBrandsList = VehicleBrands.GetAll();
            bikeAdViewModel.YearsList = Years.GetAll();
            bikeAdViewModel.ColorsList = Colors.GetAll(); // I should not sent all these colors // i should have a AJAX for it.
            bikeAdViewModel.ConditionsList = Conditions.GetAll();
            bikeAdViewModel.CurrenciesList = Currencies.GetAll();
            bikeAdViewModel.CountriesList = Countries.GetAll();
            bikeAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == seller.CountryID);
            bikeAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == seller.StateID);

            return View(bikeAdViewModel);
        }

        // POST: BikeAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,VehicleBrandID,YearID,DrivenKilometers,ColorID,Insurance,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID,SellerID,PostingTime")] BikeAdViewModel bikeAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                Ad ad = new Ad();

                ad.Title = bikeAdViewModel.Title;
                ad.ConditionID = bikeAdViewModel.ConditionID;
                ad.Description = bikeAdViewModel.Description;
                ad.CurrencyID = bikeAdViewModel.CurrencyID;
                ad.Price = bikeAdViewModel.Price;
                ad.CountryID = bikeAdViewModel.CountryID;
                ad.StateID = bikeAdViewModel.StateID;
                ad.CityID = bikeAdViewModel.CityID;

                string CurrentUserName = User.Identity.GetUserName();
                ad.SellerID = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                ad.Slug = bikeAdViewModel.Title.Replace(' ', '-');
                ad.PostingTime = DateTime.Now;

                Ads.Insert(ad);
                Ads.Commit();

                Bike bike = new Bike();

                bike.VehicleBrandID = bikeAdViewModel.VehicleBrandID;
                bike.YearID = bikeAdViewModel.YearID;
                bike.DrivenKilometers = bikeAdViewModel.DrivenKilometers;
                bike.Insurance = bikeAdViewModel.Insurance;
                bike.ColorID = bikeAdViewModel.ColorID;
                bike.AdID = ad.ID;

                Bikes.Insert(bike);
                Bikes.Commit();

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

                return RedirectToAction("Details", "BikeAds", new { id = bike.ID });
            }

            bikeAdViewModel.VehicleBrandsList = VehicleBrands.GetAll();
            bikeAdViewModel.YearsList = Years.GetAll();
            bikeAdViewModel.ColorsList = Colors.GetAll();
            bikeAdViewModel.ConditionsList = Conditions.GetAll();
            bikeAdViewModel.CurrenciesList = Currencies.GetAll();
            bikeAdViewModel.CountriesList = Countries.GetAll();
            bikeAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == bikeAdViewModel.CountryID);
            bikeAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == bikeAdViewModel.StateID);

            return View(bikeAdViewModel);
        }

        // GET: BikeAds/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike= Bikes.GetByID(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();

            bikeAdViewModel.ID = bike.ID;
            bikeAdViewModel.Title = bike.Ad.Title;
            bikeAdViewModel.VehicleBrand = bike.VehicleBrand;
            bikeAdViewModel.VehicleBrandID = bike.VehicleBrandID;
            bikeAdViewModel.Year = bike.Year;
            bikeAdViewModel.YearID = bike.YearID;
            bikeAdViewModel.Color = bike.Color;
            bikeAdViewModel.ColorID = bike.ColorID;
            bikeAdViewModel.DrivenKilometers = bike.DrivenKilometers;
            bikeAdViewModel.Insurance = bike.Insurance;
            bikeAdViewModel.Condition = bike.Ad.Condition;
            bikeAdViewModel.ConditionID = bike.Ad.ConditionID;
            bikeAdViewModel.Description = bike.Ad.Description;
            bikeAdViewModel.Currency = bike.Ad.Currency;
            bikeAdViewModel.CurrencyID = bike.Ad.CurrencyID;
            bikeAdViewModel.Price = bike.Ad.Price;
            bikeAdViewModel.Country = bike.Ad.Country;
            bikeAdViewModel.CountryID = bike.Ad.CountryID;
            bikeAdViewModel.State = bike.Ad.State;
            bikeAdViewModel.StateID = bike.Ad.StateID;
            bikeAdViewModel.City = bike.Ad.City;
            bikeAdViewModel.CityID = bike.Ad.CityID;

            bikeAdViewModel.VehicleBrandsList = VehicleBrands.GetAll();
            bikeAdViewModel.ConditionsList = Conditions.GetAll();
            bikeAdViewModel.ColorsList = Colors.GetAll();
            bikeAdViewModel.YearsList = Years.GetAll();
            bikeAdViewModel.CurrenciesList = Currencies.GetAll();
            bikeAdViewModel.CountriesList = Countries.GetAll();
            bikeAdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == bike.Ad.CountryID);
            bikeAdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == bike.Ad.StateID);

            return View(bikeAdViewModel);
        }

        // POST: BikeAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,VehicleBrandID,YearID,DrivenKilometers,ColorID,Insurance,ConditionID,Description,CurrencyID,Price,CountryID,StateID,CityID,SellerID,PostingTime")] BikeAdViewModel bikeAdViewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                Bike bike = Bikes.GetByID(bikeAdViewModel.ID);

                bike.VehicleBrandID = bikeAdViewModel.VehicleBrandID;
                bike.YearID = bikeAdViewModel.YearID;
                bike.ColorID = bikeAdViewModel.ColorID;
                bike.DrivenKilometers = bikeAdViewModel.DrivenKilometers;
                bike.Insurance = bikeAdViewModel.Insurance;

                Bikes.Update(bike);
                Bikes.Commit();

                Ad ad = Ads.GetByID(bike.AdID);

                ad.Title = bikeAdViewModel.Title;
                ad.ConditionID = bikeAdViewModel.ConditionID;
                ad.Condition = bikeAdViewModel.Condition;
                ad.Description = bikeAdViewModel.Description;
                ad.CurrencyID = bikeAdViewModel.CurrencyID;
                ad.Currency = bikeAdViewModel.Currency;
                ad.Price = bikeAdViewModel.Price;
                ad.CountryID = bikeAdViewModel.CountryID;
                ad.Country = bikeAdViewModel.Country;
                ad.StateID = bikeAdViewModel.StateID;
                ad.State = bikeAdViewModel.State;
                ad.CityID = bikeAdViewModel.CityID;
                ad.City = bikeAdViewModel.City;
                ad.Slug = bikeAdViewModel.Title.Replace(' ', '-');

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

                return RedirectToAction("Details", "BikeAds", new { id = bike.ID });
            }

            return View(bikeAdViewModel);
        }

        // GET: BikeAds/Delete/5
        [Authorize]
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

            BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();

            bikeAdViewModel.ID = bike.ID;
            bikeAdViewModel.Title = bike.Ad.Title;
            bikeAdViewModel.VehicleBrandID = bike.VehicleBrandID;
            bikeAdViewModel.VehicleBrand = bike.VehicleBrand;
            bikeAdViewModel.YearID = bike.YearID;
            bikeAdViewModel.Year = bike.Year;
            bikeAdViewModel.ColorID = bike.ColorID;
            bikeAdViewModel.Color = bike.Color;
            bikeAdViewModel.DrivenKilometers = bike.DrivenKilometers;
            bikeAdViewModel.Insurance = bike.Insurance;
            bikeAdViewModel.ConditionID = bike.Ad.ConditionID;
            bikeAdViewModel.Condition = bike.Ad.Condition;
            bikeAdViewModel.Description = bike.Ad.Description;
            bikeAdViewModel.CurrencyID = bike.Ad.CurrencyID;
            bikeAdViewModel.Currency = bike.Ad.Currency;
            bikeAdViewModel.Price = bike.Ad.Price;
            bikeAdViewModel.CountryID = bike.Ad.CountryID;
            bikeAdViewModel.Country = bike.Ad.Country;
            bikeAdViewModel.StateID = bike.Ad.StateID;
            bikeAdViewModel.State = bike.Ad.State;
            bikeAdViewModel.CityID = bike.Ad.CityID;
            bikeAdViewModel.City = bike.Ad.City;
            bikeAdViewModel.SellerID = bike.Ad.SellerID;
            bikeAdViewModel.Seller = bike.Ad.Seller;

            return View(bikeAdViewModel);
        }

        // POST: BikeAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = Bikes.GetByID(id);

            Bikes.Delete(bike);
            Bikes.Commit();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview(BikeAdViewModel bikeAdViewModel, int? id, string Review)
        {
            if (Review.Length >= 50)
            {
                Bike bike = Bikes.GetByID(id);

                if (bike == null)
                {
                    return HttpNotFound();
                }

                string CurrentUserName = User.Identity.GetUserName();

                Review newReview = new Review();

                newReview.Content = Review;
                newReview.AdID = bike.AdID;
                newReview.Ad = bike.Ad;
                newReview.PostingTime = DateTime.Now;

                Seller seller = Sellers.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

                newReview.SellerID = seller.ID;
                newReview.Seller = seller;

                newReview.ReviewStars = 5;

                Reviews.Insert(newReview);
                Reviews.Commit();

                return RedirectToAction("Details", "BikeAds", new { id = id });
            }
            return RedirectToAction("Details", "BikeAds", new { id = id });
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
