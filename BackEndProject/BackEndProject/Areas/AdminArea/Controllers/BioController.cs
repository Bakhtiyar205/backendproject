using BackEndProject.Datas;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class BioController : Controller
    {
        private readonly AppDbContext _context;

        public BioController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Setting setting = await _context.Settings.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (setting == null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != setting.Id) return BadRequest();

            try
            {
                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
                if (dbSetting.Value.ToLower().Trim() == setting.Value.ToLower().Trim())
                    return RedirectToAction(nameof(Index));

                dbSetting.Value = setting.Value;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }
    }
}
