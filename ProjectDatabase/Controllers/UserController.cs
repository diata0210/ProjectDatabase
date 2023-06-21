using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class UserController : Controller
    {
        private readonly OrderDbContext _context;

        public UserController(OrderDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            if (!User.IsInRole("AD"))
            {
                return View("~/Views/Shared/Error", "Bạn không có quyền truy cập vào trang này.");
            }
            var stores = _context.Store.ToList();
            // Đưa danh sách cửa hàng vào ViewBag
            ViewBag.Stores = new SelectList(stores, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            bool isUserExists = _context!.User!.Any(u => u.username == user.username);
            if (_context!.User!.Any(u => u.id == user.id))
            {
                ModelState.AddModelError("id", "This ID already exists.");
                return View("Create");
            }
            if (isUserExists)
            {
                ModelState.AddModelError(string.Empty, "Username already exists");
                return View("Create");
            }
            if (ModelState.IsValid)
            {
                _context!.User!.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost("{id}")]
        public IActionResult Edit(User user)
        {
            var _user = _context!.User!.FirstOrDefault(u => u.id == user.id);
            if (_user == null)
            {
                return NotFound(); // Handle when the user is not found
            }

            // Update the user properties here

            _context.SaveChanges(); // Save the changes to the database

            return RedirectToAction("Index"); // Redirect to the index page or any other desired action
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _context!.User!.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound(); // Handle when the user is not found
            }

            _context!.User!.Remove(user); // Remove the user from the database
            _context.SaveChanges(); // Save the changes to the database

            return RedirectToAction("Index"); // Redirect to the index page or any other desired action
        }

        // Các action khác không thay đổi

        public IActionResult Index()
        {
            var users = _context.User.ToList();

            return View(users);
        }

    }
}
