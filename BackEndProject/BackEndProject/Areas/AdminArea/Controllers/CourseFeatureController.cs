using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Utilities.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CourseFeatureController : Controller
    {
        private readonly AppDbContext _context;

        public CourseFeatureController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<CourseFeature> courseFeatures = await _context.CourseFeatures.Include(m=>m.Course).Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(courseFeatures);
        }

        private async Task<SelectList> GetCourses()
        {
            var course = await _context.Courses.Where(m => !m.IsDeleted).ToListAsync();

            return new SelectList(course, "Id", "Header");
        }

        public async Task<CourseFeature> GetElementById(int id)
        {
            return await _context.CourseFeatures.FindAsync(id);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCourses();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseFeature courseFeature)
        {
            ViewBag.categories = await GetCourses();
            if (!ModelState.IsValid) return View();

            await _context.CourseFeatures.AddAsync(courseFeature);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CourseFeature courseFeature = await _context.CourseFeatures.FindAsync(id);
            if (courseFeature is null) return View();
            courseFeature.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.categories = await GetCourses();
            CourseFeature courseFeature = await _context.CourseFeatures.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (courseFeature is null) NotFound();

            return View(courseFeature);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.categories = await GetCourses();
            CourseFeature courseFeature = await _context.CourseFeatures.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (courseFeature == null) return NotFound();

            return View(courseFeature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseFeature courseFeature)
        {
            ViewBag.categories = await GetCourses();
            if (!ModelState.IsValid) return View();
            if (id != courseFeature.Id) BadRequest();

            try
            {
                CourseFeature dbCoutseFeature = await _context.CourseFeatures.Where(m=>!m.IsDeleted && m.Id == id).AsNoTracking().FirstOrDefaultAsync();

                _context.CourseFeatures.Update(courseFeature);
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
