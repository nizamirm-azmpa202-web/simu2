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

        public async Task<IActionResult> Index()
        {
            var chefs = await _context.Chefs.ToListAsync();
            return View(chefs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Chef chef)
        {
            if (!ModelState.IsValid)
                return View();

            await _context.Chefs.AddAsync(chef);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);

            return View(chef);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Chef chef)
        {
            _context.Chefs.Update(chef);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);

            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}