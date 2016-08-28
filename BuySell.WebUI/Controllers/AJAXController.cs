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
    public class AJAXController : Controller
    {
        // GET: AJAX
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FillStates(int Country)
        {
            IRepositoryBase<State> statesRepo = new StatesRepository(new DataContext());
            var states = statesRepo.GetAll().ToList().Where(s => s.CountryID == Country);

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillCities(int State)
        {
            IRepositoryBase<City> citiesRepo = new CitiesRepository(new DataContext());
            var cities = citiesRepo.GetAll().ToList().Where(s => s.StateID == State);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

    }
}