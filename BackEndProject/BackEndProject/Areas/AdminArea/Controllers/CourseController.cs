using BackEndProject.Datas;
using BackEndProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BackEndProject.Utilities.Files;
using BackEndProject.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CourseController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private async Task<SelectList> GetCategories()
        {
            var category = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();

            return new SelectList(category, "Id", "CategorySection");
        }

        public async Task<Course> GetElementById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _context.Courses.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Header"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Text"].ValidationState == ModelValidationState.Invalid) return View();

            if (!course.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View(course);
            }
            if(!course.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View(course);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + course.Photo.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/course", fileName);

            await course.Photo.SaveFiles(path);

            course.Image = fileName;

            await _context.Courses.AddAsync(course);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Course course = await GetElementById(id);

            if (course == null) return NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/course", course.Image);

            Helper.DeleteFile(path);

            //_context.sliders.Remove(slider);

            course.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.categories = await GetCategories();
            Course course = await _context.Courses.FindAsync(id);

            if (course is null) NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int id, Course course)
        {
            ViewBag.categories = await GetCategories();
            Course dbCourse = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View(dbCourse);
            if (!course.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "The File Should Be Image Type");
                return View(dbCourse);
            };

            if (!course.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "Please Upload Less Than 300KB");
                return View(dbCourse);
            }

            if (dbCourse == null) NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/course", dbCourse.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + course.Photo.FileName;

            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/course", fileName);

            await course.Photo.SaveFiles(pathNew);


            course.Image = fileName;
            _context.Courses.Update(course);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int id)
        {
            Course course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return View(course);
        }
    }
}
