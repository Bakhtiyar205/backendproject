using BackEndProject.Datas;
using BackEndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserActivationController : Controller
    {
        private readonly AppDbContext _context;

        public UserActivationController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> user = await _context.Users.ToListAsync();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Active(string id)
        {
            AppUser user = await _context.Users.Where((m=>m.Id == id)).FirstOrDefaultAsync();

            user.IsActivated = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeActive(string id)
        {
            AppUser user = await _context.Users.Where((m => m.Id == id)).FirstOrDefaultAsync();

            user.IsActivated = false;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
