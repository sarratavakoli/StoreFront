using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using Version = StoreFront.DATA.EF.Models.Version; //clarification added to resolve error with Create([Bind("ID,Name")] Version version

namespace StoreFront.UI.MVC.Controllers
{
    public class VersionsController : Controller
    {
        private readonly StoreFrontContext _context;

        public VersionsController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: Versions
        public async Task<IActionResult> Index()
        {
              return _context.Versions != null ? 
                          View(await _context.Versions.ToListAsync()) :
                          Problem("Entity set 'StoreFrontContext.Versions'  is null.");
        }

        // GET: Versions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var version = await _context.Versions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (version == null)
            {
                return NotFound();
            }

            return View(version);
        }

        // GET: Versions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Versions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Version version)
        {
            if (ModelState.IsValid)
            {
                _context.Add(version);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(version);
        }

        // GET: Versions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var version = await _context.Versions.FindAsync(id);
            if (version == null)
            {
                return NotFound();
            }
            return View(version);
        }

        // POST: Versions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Version version)
        {
            if (id != version.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(version);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VersionExists(version.ID))
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
            return View(version);
        }

        // GET: Versions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Versions == null)
            {
                return NotFound();
            }

            var version = await _context.Versions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (version == null)
            {
                return NotFound();
            }

            return View(version);
        }

        // POST: Versions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Versions == null)
            {
                return Problem("Entity set 'StoreFrontContext.Versions'  is null.");
            }
            var version = await _context.Versions.FindAsync(id);
            if (version != null)
            {
                _context.Versions.Remove(version);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VersionExists(int id)
        {
          return (_context.Versions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
