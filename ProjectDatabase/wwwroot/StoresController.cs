using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.wwwroot
{
    public class StoresController : Controller
    {
        private readonly OrderDbContext _context;

        public StoresController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var orderDbContext = _context.Stores.Include(s => s.District).Include(s => s.Province);
            return View(await orderDbContext.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.District)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            ViewData["district_id"] = new SelectList(_context.Districts, "id", "id");
            ViewData["province_id"] = new SelectList(_context.Provinces, "id", "id");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,province_id,district_id,address")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["district_id"] = new SelectList(_context.Districts, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Provinces, "id", "id", store.province_id);
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["district_id"] = new SelectList(_context.Districts, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Provinces, "id", "id", store.province_id);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,name,description,province_id,district_id,address")] Store store)
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
            ViewData["district_id"] = new SelectList(_context.Districts, "id", "id", store.district_id);
            ViewData["province_id"] = new SelectList(_context.Provinces, "id", "id", store.province_id);
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.District)
                .Include(s => s.Province)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'OrderDbContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(string id)
        {
          return (_context.Stores?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
