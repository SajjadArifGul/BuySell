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

            //Get Bikes from DB
            List<Bike> BikesList = Bikes.GetAll().ToList();
            List<CellPhone> CellPhonesList = CellPhones.GetAll().ToList();
            List<Laptop> LaptopsList = Laptops.GetAll().ToList();

            //only send 3 Bikes to Homepage
            for (int i = 0; i < 3; i++)
            {
                BikeAdViewModel bikeAdViewModel = new BikeAdViewModel();

                bikeAdViewModel.ID = BikesList[i].ID;
                bikeAdViewModel.Title = BikesList[i].Ad.Title;
                bikeAdViewModel.VehicleBrandID = BikesList[i].VehicleBrandID;
                bikeAdViewModel.VehicleBrand = BikesList[i].VehicleBrand;
                bikeAdViewModel.YearID = BikesList[i].YearID;
                bikeAdViewModel.Year = BikesList[i].Year;
                bikeAdViewModel.DrivenKilometers = BikesList[i].DrivenKilometers;
                bikeAdViewModel.ColorID = BikesList[i].ColorID;
                bikeAdViewModel.Color = BikesList[i].Color;
                bikeAdViewModel.Insurance = BikesList[i].Insurance;
                bikeAdViewModel.Condition = BikesList[i].Ad.Condition;
                bikeAdViewModel.ConditionID = BikesList[i].Ad.ConditionID;
                bikeAdViewModel.Description = BikesList[i].Ad.Description;
                bikeAdViewModel.Currency = BikesList[i].Ad.Currency;
                bikeAdViewModel.CurrencyID = BikesList[i].Ad.CurrencyID;
                bikeAdViewModel.Price = BikesList[i].Ad.Price;
                bikeAdViewModel.Country = BikesList[i].Ad.Country;
                bikeAdViewModel.CountryID = BikesList[i].Ad.CountryID;
                bikeAdViewModel.StateID = BikesList[i].Ad.StateID;
                bikeAdViewModel.State = BikesList[i].Ad.State;
                bikeAdViewModel.CityID = BikesList[i].Ad.CityID;
                bikeAdViewModel.City = BikesList[i].Ad.City;
                bikeAdViewModel.SellerID = BikesList[i].Ad.SellerID;
                bikeAdViewModel.Seller = BikesList[i].Ad.Seller;

                bikeAdViewModel.Images = BikesList[i].Ad.Images;

                bikeAdViewModel.Reviews = BikesList[i].Ad.Reviews;

                bikeAdViewModels.Add(bikeAdViewModel);
            }

            for (int i = 0; i < 3; i++)
            {
                CellPhoneAdViewModel cellPhoneAdViewModel = new CellPhoneAdViewModel();

                cellPhoneAdViewModel.ID = CellPhonesList[i].ID;
                cellPhoneAdViewModel.Title = CellPhonesList[i].Ad.Title;
                cellPhoneAdViewModel.AccessoryBrandID = CellPhonesList[i].AccessoryBrandID;
                cellPhoneAdViewModel.AccessoryBrand = CellPhonesList[i].AccessoryBrand;
                cellPhoneAdViewModel.OperatingSystem = CellPhonesList[i].OperatingSystem;
                cellPhoneAdViewModel.Condition = CellPhonesList[i].Ad.Condition;
                cellPhoneAdViewModel.ConditionID = CellPhonesList[i].Ad.ConditionID;
                cellPhoneAdViewModel.Description = CellPhonesList[i].Ad.Description;
                cellPhoneAdViewModel.Currency = CellPhonesList[i].Ad.Currency;
                cellPhoneAdViewModel.CurrencyID = CellPhonesList[i].Ad.CurrencyID;
                cellPhoneAdViewModel.Price = CellPhonesList[i].Ad.Price;
                cellPhoneAdViewModel.Country = CellPhonesList[i].Ad.Country;
                cellPhoneAdViewModel.CountryID = CellPhonesList[i].Ad.CountryID;
                cellPhoneAdViewModel.StateID = CellPhonesList[i].Ad.StateID;
                cellPhoneAdViewModel.State = CellPhonesList[i].Ad.State;
                cellPhoneAdViewModel.CityID = CellPhonesList[i].Ad.CityID;
                cellPhoneAdViewModel.City = CellPhonesList[i].Ad.City;
                cellPhoneAdViewModel.SellerID = CellPhonesList[i].Ad.SellerID;
                cellPhoneAdViewModel.Seller = CellPhonesList[i].Ad.Seller;

                cellPhoneAdViewModel.Images = CellPhonesList[i].Ad.Images;

                cellPhoneAdViewModel.Reviews = CellPhonesList[i].Ad.Reviews;

                cellPhoneAdViewModels.Add(cellPhoneAdViewModel);
            }

            for (int i = 0; i < 3; i++)
            {
                LaptopAdViewModel laptopAdViewModel = new LaptopAdViewModel();

                laptopAdViewModel.ID = LaptopsList[i].ID;
                laptopAdViewModel.Title = LaptopsList[i].Ad.Title;
                laptopAdViewModel.AccessoryBrandID = LaptopsList[i].AccessoryBrandID;
                laptopAdViewModel.AccessoryBrand = LaptopsList[i].AccessoryBrand;
                laptopAdViewModel.OperatingSystem = LaptopsList[i].OperatingSystem;
                laptopAdViewModel.Ram = LaptopsList[i].Ram;
                laptopAdViewModel.Processor = LaptopsList[i].Processor;
                laptopAdViewModel.HardDisk = LaptopsList[i].HardDisk;
                laptopAdViewModel.Condition = LaptopsList[i].Ad.Condition;
                laptopAdViewModel.ConditionID = LaptopsList[i].Ad.ConditionID;
                laptopAdViewModel.Description = LaptopsList[i].Ad.Description;
                laptopAdViewModel.Currency = LaptopsList[i].Ad.Currency;
                laptopAdViewModel.CurrencyID = LaptopsList[i].Ad.CurrencyID;
                laptopAdViewModel.Price = LaptopsList[i].Ad.Price;
                laptopAdViewModel.Country = LaptopsList[i].Ad.Country;
                laptopAdViewModel.CountryID = LaptopsList[i].Ad.CountryID;
                laptopAdViewModel.StateID = LaptopsList[i].Ad.StateID;
                laptopAdViewModel.State = LaptopsList[i].Ad.State;
                laptopAdViewModel.CityID = LaptopsList[i].Ad.CityID;
                laptopAdViewModel.City = LaptopsList[i].Ad.City;
                laptopAdViewModel.SellerID = LaptopsList[i].Ad.SellerID;
                laptopAdViewModel.Seller = LaptopsList[i].Ad.Seller;

                laptopAdViewModel.Images = LaptopsList[i].Ad.Images;

                laptopAdViewModel.Reviews = LaptopsList[i].Ad.Reviews;

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