using BuySell.Contracts.Repositories;
using BuySell.DAL.Data;
using BuySell.DAL.Repository;
using BuySell.Models;
using BuySell.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuySell.WebUI.Controllers
{
    [Authorize]
    public class VehicleAdController : Controller
    {
        // GET: VehicleAd
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post()
        {
            IRepositoryBase<VehicleBrand> vehicleBrands = new VehicleBrandsRepository(new DataContext());
            ViewBag.VehicleBrandsList = vehicleBrands.GetAll();

            IRepositoryBase<Currency> currencies = new CurrenciesRepository(new DataContext());
            ViewBag.CurrenciesList = currencies.GetAll();

            IRepositoryBase<Country> countries = new CountriesRepository(new DataContext());
            ViewBag.CountriesList = countries.GetAll();

            return View();
        }

        public ActionResult FillStates(int Country)
        {
            IRepositoryBase<State> statesRepo = new StatesRepository(new DataContext());
            var states = statesRepo.GetAll().ToList().Where(s=>s.CountryID == Country);
            
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillCities(int State)
        {
            IRepositoryBase<City> citiesRepo = new CitiesRepositories(new DataContext());
            var cities = citiesRepo.GetAll().ToList().Where(s => s.StateID == State);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }
    }
}