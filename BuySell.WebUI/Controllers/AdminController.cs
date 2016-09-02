using BuySell.Contracts.Repositories;
using BuySell.DAL.Data;
using BuySell.DAL.Repository;
using BuySell.Models;
using BuySell.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuySell.WebUI.Controllers
{
    public class AdminController : Controller
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

        public AdminController()
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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LaptopAds()
        {
            List<Laptop> LaptopsList = Laptops.GetAll().Take(12).OrderByDescending(b => b.Ad.PostingTime).ToList();

            List<LaptopAdViewModel> laptopAdViewModels = new List<LaptopAdViewModel>();

            foreach (Laptop laptop in LaptopsList)
            {
                LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

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

                laptopAdViewModel.Images = laptop.Ad.Images;

                laptopAdViewModel.Reviews = laptop.Ad.Reviews;

                laptopAdViewModels.Add(laptopAdViewModel);
            }

            return View(laptopAdViewModels);
        }
    }
}