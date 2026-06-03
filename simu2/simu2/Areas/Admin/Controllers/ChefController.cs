using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simu2.Data;
using simu2.Models;

namespace simu2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _context;

        public ChefController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Chef/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var chefs = await _context.Chefs.ToListAsync();
                return View(chefs);
            }
            catch
            {
                return View(new List<Chef>());
            }
        }

        // GET: Admin/Chef/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var chef = await _context.Chefs.FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
                return NotFound();

            return View(chef);
        }

        // GET: Admin/Chef/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Chef/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Job,Image")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(chef);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error creating chef: " + ex.Message);
                }
            }
            return View(chef);
        }

        // GET: Admin/Chef/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
                return NotFound();

            return View(chef);
        }

        // POST: Admin/Chef/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Job,Image")] Chef chef)
        {
            if (id != chef.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chef);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _context.Chefs.AnyAsync(c => c.Id == chef.Id);
                    if (!exists)
                        return NotFound();
                    else
                        throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating chef: " + ex.Message);
                }
            }
            return View(chef);
        }

        // GET: Admin/Chef/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var chef = await _context.Chefs.FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
                return NotFound();

            return View(chef);
        }

        // POST: Admin/Chef/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
                return NotFound();

            try
            {
                _context.Chefs.Remove(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting chef: " + ex.Message);
                return View(chef);
            }
        }
    }
}
