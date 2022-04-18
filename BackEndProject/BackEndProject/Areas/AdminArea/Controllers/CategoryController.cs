using BackEndProject.Datas;
using BackEndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (category is null) NotFound();

            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid) return View();
            if (id != category.Id) BadRequest();

            try
            {
                Category dbCategory = await _context.Categories.FindAsync(id);
                if (dbCategory.CategorySection.ToLower().Trim() == category.CategorySection.ToLower().Trim())
                {
                    return RedirectToAction(nameof(Index));
                }

                dbCategory.CategorySection = category.CategorySection;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category is null) return View();
            category.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
