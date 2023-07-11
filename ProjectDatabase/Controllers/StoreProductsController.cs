using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    [Authorize]
    public class StoreProductsController : Controller
    {
        private readonly OrderDbContext _context;

        public StoreProductsController(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<UserStoreRollModelView> GetCurrentUserInfo()
        {
            var userInfo = new UserStoreRollModelView
            {
                UserName = User.FindFirstValue("FullName"),
               Role = User.FindFirstValue(ClaimTypes.Role),
               Store = User.FindFirstValue("StoreId")
            };

            return userInfo;
        }

        // GET: StoreProducts
        public async Task<IActionResult> Index()
        {

            var orderDbContext = _context.Store_products.Include(s => s.Product).Include(s => s.Store);
            UserStoreRollModelView userInfo = await GetCurrentUserInfo();
            ViewBag.UserInfo = userInfo;

            if (userInfo.Role == "2")
            {
                orderDbContext = orderDbContext.Where(s => s.store_id.ToString() == userInfo.Store)
                               .Include(s => s.Store);

            }

            var newlist = new Store_productsStoreModelView
            {
                Stores = await _context.Stores.ToListAsync(),
                Products = await orderDbContext.ToListAsync()
            };

            
            return View(newlist);
        }



        // GET: StoreProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Store_products == null)
            {
                return NotFound();
            }

            var store_product = await _context.Store_products
                .Include(s => s.Product)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store_product == null)
            {
                return NotFound();
            }

            return View(store_product);
        }

        // GET: StoreProducts/Create
        [Authorize(Roles = "1")]
        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.Products, "id", "name");
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address");
            return View();
        }

        // POST: StoreProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]

        public async Task<IActionResult> Create([Bind("id,store_id,product_id,quantity")] Store_product store_product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store_product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["product_id"] = new SelectList(_context.Products, "id", "name", store_product.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address", store_product.store_id);
            return View(store_product);
        }

        // GET: StoreProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Store_products == null)
            {
                return NotFound();
            }

            var store_product = await _context.Store_products.FindAsync(id);
            if (store_product == null)
            {
                return NotFound();
            }
            ViewData["product_id"] = new SelectList(_context.Products, "id", "name", store_product.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address", store_product.store_id);
            return View(store_product);
        }

        // POST: StoreProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,store_id,product_id,quantity")] Store_product store_product)
        {
            if (id != store_product.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store_product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Store_productExists(store_product.id))
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
            ViewData["product_id"] = new SelectList(_context.Products, "id", "name", store_product.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address", store_product.store_id);
            return View(store_product);
        }

        // GET: StoreProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Store_products == null)
            {
                return NotFound();
            }

            var store_product = await _context.Store_products
                .Include(s => s.Product)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.id == id);
            if (store_product == null)
            {
                return NotFound();
            }

            return View(store_product);
        }

        // POST: StoreProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Store_products == null)
            {
                return Problem("Entity set 'OrderDbContext.Store_products'  is null.");
            }
            var store_product = await _context.Store_products.FindAsync(id);
            if (store_product != null)
            {
                _context.Store_products.Remove(store_product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Store_productExists(int id)
        {
          return (_context.Store_products?.Any(e => e.id == id)).GetValueOrDefault();
        }
        


    }
}
