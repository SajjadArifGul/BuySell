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

            homeViewModel.Bikes = Bikes.GetAll();
            homeViewModel.CellPhones = CellPhones.GetAll();
            homeViewModel.Laptops = Laptops.GetAll();

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