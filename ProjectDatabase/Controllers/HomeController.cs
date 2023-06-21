using Microsoft.AspNetCore.Mvc;
using ProjectDatabase.Models;
using System.Diagnostics;

namespace ProjectDatabase.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Product() { 
            return View();
        }
        public IActionResult Customer() {
            return View();
        }
       
        public IActionResult Revenue() {
            return View();
        }
        
    }
}