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
    public class VersionsProductsController : Controller
    {
        private readonly StoreFrontContext _context;

        public VersionsProductsController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: VersionsProducts
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.VersionsProducts.Include(v => v.Product);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: VersionsProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VersionsProducts == null)
            {
                return NotFound();
            }

            var versionsProduct = await _context.VersionsProducts
                .Include(v => v.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (versionsProduct == null)
            {
                return NotFound();
            }

            return View(versionsProduct);
        }

        // GET: VersionsProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: VersionsProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Properties,Image,UnitsInStock,UnitsOnOrder,IsActive,Version")] VersionsProduct versionsProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(versionsProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", versionsProduct.ProductId);
            return View(versionsProduct);
        }

        // GET: VersionsProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VersionsProducts == null)
            {
                return NotFound();
            }

            var versionsProduct = await _context.VersionsProducts.FindAsync(id);
            if (versionsProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", versionsProduct.ProductId);
            return View(versionsProduct);
        }

        // POST: VersionsProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Properties,Image,UnitsInStock,UnitsOnOrder,IsActive,Version")] VersionsProduct versionsProduct)
        {
            if (id != versionsProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(versionsProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VersionsProductExists(versionsProduct.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", versionsProduct.ProductId);
            return View(versionsProduct);
        }

        // GET: VersionsProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VersionsProducts == null)
            {
                return NotFound();
            }

            var versionsProduct = await _context.VersionsProducts
                .Include(v => v.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (versionsProduct == null)
            {
                return NotFound();
            }

            return View(versionsProduct);
        }

        // POST: VersionsProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VersionsProducts == null)
            {
                return Problem("Entity set 'StoreFrontContext.VersionsProducts'  is null.");
            }
            var versionsProduct = await _context.VersionsProducts.FindAsync(id);
            if (versionsProduct != null)
            {
                _context.VersionsProducts.Remove(versionsProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VersionsProductExists(int id)
        {
          return (_context.VersionsProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
