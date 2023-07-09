using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
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
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orderDbContext = _context.Orders.Include(o => o.Customer).Include(o => o.Order_type).Include(o => o.Store).Include(o => o.User);
            UserStoreRollModelView userInfo =  await GetCurrentUserInfo();
            ViewBag.userInfo = userInfo;
            if (User.IsInRole("2"))
            {
                orderDbContext = orderDbContext.Where(s => s.store_id.ToString() == userInfo.Store)
                               .Include(o => o.Order_type).Include(o => o.Store).Include(o => o.User);
            }

            var newList = new OrderStoreModelView
            {
                Orders = await orderDbContext.ToListAsync(),
                Stores = await _context.Stores.ToListAsync(),
                Orderlines = await _context.Orderlines.ToListAsync()
            };

            return View(newList);
        }

            // GET: Orders/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Order_type)
                .Include(o => o.Store)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [HttpPost]
        public IActionResult GetStoreId(string storeId)
        {
            var result = _context.Store_products
                .Where(sp => sp.store_id.ToString() == storeId)
                .Include(sp => sp.Product)
                .Select(sp => new
                {
                    Value = sp.Product.id.ToString(),
                    Text = sp.Product.name
                })
                .ToList();

            return Json(result);
        }

        [HttpPost]
        public IActionResult GetQuantityProduct(string productId, string storeId)
        {
            var quantity = _context.Store_products
                .Where(x => x.product_id.ToString() == productId && x.store_id.ToString() == storeId)
                .Select(x => x.quantity)
                .FirstOrDefault();
            return Json(quantity);
        }

        public IActionResult Create()
        {


            ViewData["customer_id"] = new SelectList(_context.Customers, "id", "name");
            ViewData["type"] = new SelectList(_context.Order_types, "id", "name");

            var model = new OrderOrderlineModelView();

            var userId = _context.Users!.Where(x => x.username == User.Identity!.Name).Select(x => x.id).FirstOrDefault();
            var userName = User.Identity!.Name;

            ViewData["create_by"] = new SelectListItem { 
                Value = userId.ToString(),
                Text = userName 
            };

            if (User.IsInRole("1"))
            {
                ViewData["store_id"] = new SelectList(_context.Stores, "id", "name");
                model.Store_product = new SelectList(_context.Store_products
                   .Include(sp => sp.Product)
                   .Select(sp => new SelectListItem
                   {
                       Value = sp.Product!.id.ToString(),
                       Text = sp.Product.name
                   })
                   .ToList(), "Value", "Text");
            }

            {
                var store_id = _context!.Users!
                                       .Where(x => x.username == userName)
                                       .Select(s => s.store_id)
                                       .FirstOrDefault();
                var store_name = _context.Stores
                                         .Where(x => x.id == store_id)
                                         .Select(x => x.name)
                                         .FirstOrDefault();
                ViewData["store_id"] = new SelectListItem { Value = store_id.ToString(), Text = store_name };
                model.Store_product = new SelectList(_context.Store_products
                   .Where(sp => sp.store_id == store_id)
                   .Include(sp => sp.Product)
                   .Select(sp => new SelectListItem
                   {
                       Value = sp.Product!.id.ToString(),
                       Text = sp.Product.name
                   })
                   .ToList(), "Value", "Text");

                model.Store = new SelectList(_context.Stores
               .Select(st => new SelectListItem
               {
                   Value = st.id.ToString(),
                   Text = st.name
               })
               .ToList(), "Value", "Text");
            }


            return View(model);
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderOrderlineModelView orderViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        // Thêm order vào database 
                        orderViewModel.Order!.create_at = orderViewModel.Order.create_at.ToUniversalTime();
                        _context.Add(orderViewModel.Order);
                        await _context.SaveChangesAsync();


                        // lấy id của order vừa thêm 
                        var orderID = orderViewModel.Order.id;

                        // gán orderId cho các orderlines 
                        foreach (  var orderline in orderViewModel!.Orderline!)
                        {
                            orderline.order_id = orderID;

                            // lưu vào orderline
                            _context.Add(orderline);
                        }

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception)
                    {
                        // Rollback transaction trong trường hợp có lỗi
                        transaction.Rollback();
                        throw;
                    }
                }
                
            }
            var model = new OrderOrderlineModelView();

            model.Store_product = new SelectList(_context.Store_products
             .Include(sp => sp.Product)
             .Select(sp => new SelectListItem
             {
                 Value = sp.Product.id.ToString(),
                 Text = sp.Product.name
             })
            .ToList(), "Value", "Text");

            ViewData["customer_id"] = new SelectList(_context.Customers, "id", "name", orderViewModel!.Order!.customer_id);
            ViewData["type"] = new SelectList(_context.Order_types, "id", "name", orderViewModel!.Order.type);
            var userId = _context.Users!.Where(x => x.username == User.Identity!.Name).Select(x => x.id).FirstOrDefault();
            var userName = User.Identity!.Name;

            ViewData["create_by"] = new SelectListItem
            {
                Value = userId.ToString(),
                Text = userName
            };
            if (User.IsInRole("1"))
            {
                model.Store = new SelectList(_context.Stores
                .Select(st => new SelectListItem
                {
                    Value = st.id.ToString(),
                    Text = st.name
                }).ToList(), "Value", "Text");
            }
            else
            {
                var store_id = _context!.Users!
                                       .Where(x => x.username == userName)
                                       .Select(s => s.store_id)
                                       .FirstOrDefault();
                var store_name = _context.Stores
                                         .Where(x => x.id == store_id)
                                         .Select(x => x.name)
                                         .FirstOrDefault();
                ViewData["store_id"] = new SelectListItem { Value = store_id.ToString(), Text = store_name };
            }
            return View(orderViewModel);

        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["customer_id"] = new SelectList(_context.Customers, "id", "name", order.customer_id);
            ViewData["type"] = new SelectList(_context.Order_types, "id", "name", order.type);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address", order.store_id);
            ViewData["create_by"] = new SelectList(_context.Users, "id", "name", order.create_by);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,total_price,status_id,note,create_by,store_id,create_at,update_at,type,customer_id,discount_membership,final_price")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
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
            ViewData["customer_id"] = new SelectList(_context.Customers, "id", "name", order.customer_id);
            ViewData["type"] = new SelectList(_context.Order_types, "id", "name", order.type);
            ViewData["store_id"] = new SelectList(_context.Stores, "id", "address", order.store_id);
            ViewData["create_by"] = new SelectList(_context.Users, "id", "name", order.create_by);
            return View(order);
        }
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Order_type)
                .Include(o => o.Store)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'OrderDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
