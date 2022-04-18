using BackEndProject.Datas;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BackEndProject.Utilities.Helpers.Helper;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class UserActivationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public UserActivationController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> user = await _context.Users.ToListAsync();
            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsModerator(string Id)
        {
            var moderator = await _context.Users.Where(m => m.Id == Id).FirstOrDefaultAsync();

            await _userManager.AddToRoleAsync(moderator, UserRoles.Moderator.ToString());

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

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
