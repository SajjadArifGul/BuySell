using BuySell.Contracts.Repositories;
using BuySell.DAL.Data;
using BuySell.DAL.Repository;
using BuySell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuySell.WebUI.Controllers
{
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
    }
}