using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly StoreFrontContext _context;

        public OrderProductsController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: OrderProducts
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.OrderProducts.Include(o => o.IdNavigation).Include(o => o.Order);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: OrderProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderProducts == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts
                .Include(o => o.IdNavigation)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // GET: OrderProducts/Create
        public IActionResult Create()
        {
            ViewData["ID"] = new SelectList(_context.VersionsProducts, "ID", "ID");
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ShipAddress");
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderID,ProductID,UnitQuantity,UnitType,ProductPrice")] OrderProduct orderProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.VersionsProducts, "ID", "ID", orderProduct.ID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ShipAddress", orderProduct.OrderID);
            return View(orderProduct);
        }

        // GET: OrderProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderProducts == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(_context.VersionsProducts, "ID", "ID", orderProduct.ID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ShipAddress", orderProduct.OrderID);
            return View(orderProduct);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrderID,ProductID,UnitQuantity,UnitType,ProductPrice")] OrderProduct orderProduct)
        {
            if (id != orderProduct.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductExists(orderProduct.ID))
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
            ViewData["ID"] = new SelectList(_context.VersionsProducts, "ID", "ID", orderProduct.ID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ShipAddress", orderProduct.OrderID);
            return View(orderProduct);
        }

        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderProducts == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.OrderProducts
                .Include(o => o.IdNavigation)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderProducts == null)
            {
                return Problem("Entity set 'StoreFrontContext.OrderProducts'  is null.");
            }
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct != null)
            {
                _context.OrderProducts.Remove(orderProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductExists(int id)
        {
          return (_context.OrderProducts?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
