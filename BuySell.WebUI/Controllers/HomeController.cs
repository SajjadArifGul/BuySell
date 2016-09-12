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
    public class HomeController : Controller
    {
        DataContext myDataContext = new DataContext();

        IRepositoryBase<Bike> Bikes;
        IRepositoryBase<CellPhone> CellPhones;
        IRepositoryBase<Laptop> Laptops;

        public HomeController()
        {
            Bikes = new BikesRepository(myDataContext);
            CellPhones = new CellPhonesRepository(myDataContext);
            Laptops = new LaptopsRepository(myDataContext);
        }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            List<BikeAdViewModel> bikeAdViewModels = new List<BikeAdViewModel>();
            List<LaptopAdViewModel> laptopAdViewModels = new List<LaptopAdViewModel>();
            List<CellPhoneAdViewModel> cellPhoneAdViewModels = new List<CellPhoneAdViewModel>();

            //Get from DB - only send 3 items from all catgories to Homepage
            List<Bike> BikesList = Bikes.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(3).ToList();
            List<CellPhone> CellPhonesList = CellPhones.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(3).ToList();
            List<Laptop> LaptopsList = Laptops.GetAll().OrderByDescending(b => b.Ad.PostingTime).Take(3).ToList();

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

                laptopAdViewModel.Images = laptop.Ad.Images;

                laptopAdViewModel.Reviews = laptop.Ad.Reviews;

                laptopAdViewModels.Add(laptopAdViewModel);
            }

            homeViewModel.Bikes = bikeAdViewModels;
            homeViewModel.CellPhones = cellPhoneAdViewModels;
            homeViewModel.Laptops = laptopAdViewModels;

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}