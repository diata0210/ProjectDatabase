using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fable.React;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    [Authorize(Roles = "1")]
    public class UsersController : Controller
    {
        private readonly OrderDbContext _context;

        public UsersController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var orderDbContext = _context!.Users!
                .Include(u => u.Role)
                .Include(u => u.Store);

            var viewModel = new UserStoreViewModel
            {
                Users = await orderDbContext.ToListAsync(),
                Stores = await _context.Stores.ToListAsync()
            };

            return View(viewModel);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Store)
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var users = _context!.Users!.Select(u => u.username).ToList();
            ViewData["Users"] = users;

            // Lấy danh sách số điện thoại từ cơ sở dữ liệu
            var phones = _context!.Users!.Select(u => u.phone).ToList();
            ViewData["Phones"] = phones;
            ViewData["role_id"] = new SelectList(_context.Roles, "id", "name");
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,name,phone,role_id,store_id")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["role_id"] = new SelectList(_context.Roles, "id", "name", user.role_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "name", user.store_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["role_id"] = new SelectList(_context.Roles, "id", "name", user.role_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "name", user.store_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,name,phone,role_id,store_id")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["role_id"] = new SelectList(_context.Roles, "id", "name", user.role_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "name", user.store_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Store)
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'OrderDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
