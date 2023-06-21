using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    public class StoreController : Controller
    {
        private readonly OrderDbContext _context;

        public StoreController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: Store
        public async Task<IActionResult> Index()
        {
            var orderDbContext = _context.Store.Include(s => s.District).Include(s => s.Province);
            return View(await orderDbContext.ToListAsync());
        }

        // GET: Store/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .Include(s => s.District)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Store/Create
        public IActionResult Create()
        {
            ViewData["district_id"] = new SelectList(_context.District, "id", "id");
            ViewData["province_id"] = new SelectList(_context.Province, "id", "id");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,province_id,district_id")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["district_id"] = new SelectList(_context.District, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Province, "id", "id", store.province_id);
            return View(store);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["district_id"] = new SelectList(_context.District, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Province, "id", "id", store.province_id);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,name,description,province_id,district_id")] Store store)
        {
            if (id != store.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.id))
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
            ViewData["district_id"] = new SelectList(_context.District, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Province, "id", "id", store.province_id);
            return View(store);
        }

        // GET: Store/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Store == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .Include(s => s.District)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Store == null)
            {
                return Problem("Entity set 'OrderDbContext.Store'  is null.");
            }
            var store = await _context.Store.FindAsync(id);
            if (store != null)
            {
                _context.Store.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(string id)
        {
          return (_context.Store?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
