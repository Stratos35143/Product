using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Product.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Controllers
{
    public class ProductmastersController : Controller
    {
        private readonly ProductContext _context;

        public ProductmastersController(ProductContext context)
        {
            _context = context;
        }

        // GET: Productmasters
        public async Task<IActionResult> Index(string? stringSearch , string? type, int? status)
        {
            IQueryable<Productmaster> query = _context.Productmasters
                .Include(p => p.Producttype);

            if (!string.IsNullOrWhiteSpace(stringSearch))
            {
                query = query.Where(a => a.Productname != null && a.Productname.ToLower().Contains(stringSearch.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(t => t.Producttypeid == type);
            }

            if (status.HasValue)
            {
                query = query.Where(s => s.Productstatus == status.Value);
            }

            List<Productmaster> productmasters = query.ToList();
            return View(productmasters);
        }

        // GET: Productmasters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productmaster = await _context.Productmasters
                .Include(p => p.Producttype)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (productmaster == null)
            {
                return NotFound();
            }

            return View(productmaster);
        }

        // GET: Productmasters/Create
        public IActionResult Create()
        {
            ViewData["Producttypeid"] = new SelectList(_context.Producttypes, "Producttypeid", "Producttypename");
            return View();
        }

        // POST: Productmasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Productmaster productmaster)
        {
            ModelState.Remove("Productid");
            ModelState.Remove("Createdate");
            ModelState.Remove("Producttype");

            //    bool isDuplicate = await _context.Productmasters
            //.AnyAsync(p => p.Productid == productmaster.Productid);
            //    if (isDuplicate)
            //    {
            //        ModelState.AddModelError("Productid", "Productid already exists.");
            //    }

            if (ModelState.IsValid)
            {
                bool success = productmaster.CreateProduct(_context);
                if (success) {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["Producttypeid"] = new SelectList(_context.Producttypes, "Producttypeid", "Producttypename", productmaster.Producttypeid);
            return View(productmaster);
        }

        // GET: Productmasters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productmaster = await _context.Productmasters
                .FindAsync(id);
            if (productmaster == null)
            {
                return NotFound();
            }
            ViewData["Producttypeid"] = new SelectList(_context.Producttypes, "Producttypeid", "Producttypename", productmaster.Producttypeid);
            return View(productmaster);
        }

        // POST: Productmasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Productmaster productmaster)
        {
            ModelState.Remove("Producttype");

            if (id != productmaster.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool success = productmaster.EditProduct(_context);
                    if (success) {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductmasterExists(productmaster.Productid))
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
            ViewData["Producttypeid"] = new SelectList(_context.Producttypes, "Producttypeid", "Producttypeid", productmaster.Producttypeid);
            return View(productmaster);
        }

        // GET: Productmasters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productmaster = await _context.Productmasters
                .Include(p => p.Producttype)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (productmaster == null)
            {
                return NotFound();
            }

            return View(productmaster);
        }

        // POST: Productmasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productmaster = await _context.Productmasters.FindAsync(id);
            if (productmaster != null)
            {
                productmaster.DeleteProduct(_context);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductmasterExists(string id)
        {
            return _context.Productmasters.Any(e => e.Productid == id);
        }
    }
}
