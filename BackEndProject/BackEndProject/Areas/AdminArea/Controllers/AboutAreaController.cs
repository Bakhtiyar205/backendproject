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

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AboutAreaController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AboutAreaController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }



        public async Task<IActionResult> Index()
        {
            List<AboutArea> about = await _context.Abouts.Where(m => !m.IsDeleted).OrderByDescending(m=>m.Id).ToListAsync();
            return View(about);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutArea about)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Header"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["TextSecond"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Text"].ValidationState == ModelValidationState.Invalid) return View();

            if (!about.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View();
            }

            if (!about.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + about.Photo.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/about", fileName);

            await about.Photo.SaveFiles(path);

            about.Image = fileName;

            await _context.Abouts.AddAsync(about);

            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        public async Task<AboutArea> GetElementById(int id)
        {
            return await _context.Abouts.FindAsync(id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            AboutArea about = await _context.Abouts.FindAsync(id);

            if (about is null) return NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/about", about.Image);

            Helper.DeleteFile(path);

            //_context.about.Remove(slider);

            about.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            AboutArea about = await _context.Abouts
                                    .Where(m => !m.IsDeleted && m.Id == id)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

            if (about is null) NotFound();

            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutArea about)
        {
            AboutArea dbAbout = await _context.Abouts
                                    .Where(m => !m.IsDeleted && m.Id == id)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            if (!about.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "The File Should Be Image Type");
                return View(dbAbout);
            };
            if (!about.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "Please upload less than 300KB");
                return View();
            }

            if (dbAbout == null) NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/about", dbAbout.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + about.Photo.FileName;

            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/about", fileName);

            await about.Photo.SaveFiles(pathNew);

            about.Image = fileName;

            _context.Abouts.Update(about);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int id)
        {
            AboutArea aboutArea = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return View(aboutArea);
        }
    }
}
