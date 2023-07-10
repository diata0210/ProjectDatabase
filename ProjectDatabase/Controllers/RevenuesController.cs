using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Licensing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Controllers
{
    public class RevenuesController : Controller
    {
        private readonly OrderDbContext _context;

        public RevenuesController(OrderDbContext context)
        {
            _context = context;
        }
        // GET: Revenues
        public async Task<IActionResult> Index()
        {
            var orderDbContext = _context.Revenues.Include(r => r.Store);

            var newlist = new StoreRevenueModelView
            {
                Stores = await _context.Stores.ToListAsync(),
                Revenues = await orderDbContext.ToListAsync()
            };
            return View(newlist);
        }

    }
}
