using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simu2.Data;
using simu2.Models;

namespace simu2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
            
        }

        public async  Task< IActionResult> Index()
        {
            var datas = await _context.Chefs.ToListAsync();

            return View(datas);
        }

        
    }
}
