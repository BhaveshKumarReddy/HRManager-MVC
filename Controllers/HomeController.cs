using HRM_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HRM_DB_ProjectContext db;

        public HomeController(ILogger<HomeController> logger, HRM_DB_ProjectContext _db)
        {
            _logger = logger;
            db = _db;
        }
        public IActionResult handleException(string message = "Exception")
        {
            ViewBag.Emessage = message;
            return View();
        }
        public IActionResult Index()
        {
            try
            {
                ViewBag.all_locations = db.LocationLists.ToList();
                if (HttpContext.Session.GetString("Email") != null)
                {
                    string email = HttpContext.Session.GetString("Email");
                    string location_name = db.HrLists.Include(hrl => hrl.Location).SingleOrDefault(x => x.HrEmail == email).Location.LocationName;
                    ViewBag.location = location_name;
                    return View();
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
