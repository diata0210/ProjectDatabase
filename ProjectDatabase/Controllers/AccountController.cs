using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjectDatabase.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ProjectDatabase.Controllers
{
    public class AccountController : Controller
    {
        private readonly OrderDbContext _context;
        public AccountController(OrderDbContext order) {
            _context = order;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
         public IActionResult Login(User user){

            if(user.username == null && user.password == null)
            {
                ViewBag.LoginStatus = -1;
                return View();
            }
            if (user.password == null)
            {
                ViewBag.LoginStatus = -2;
                return View();
            }
            if (user.username == null)
            {
                ViewBag.LoginStatus = -3;
                return View();
            }

            var _user = _context!.User!.Where(m => m.username == user.username && m.password == user.password).FirstOrDefault();
            if(_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.username),
            new Claim("FullName", _user.name),
            new Claim(ClaimTypes.Role, _user.role_id),
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                  
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
        public IActionResult Logout ()
        {
           HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
