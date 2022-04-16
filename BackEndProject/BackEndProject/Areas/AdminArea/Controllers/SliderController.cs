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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.Where(m=>!m.IsDeleted).AsNoTracking().ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Header"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["InnerText"].ValidationState == ModelValidationState.Invalid) return View();



            if (!slider.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View();
            }

            if (!slider.Photos.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photos.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/slider", fileName);

            await slider.Photos.SaveFiles(path);

            slider.Image = fileName;

            await _context.Sliders.AddAsync(slider);

            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        public async Task<Slider> GetElementById(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await GetElementById(id);

            if (slider == null) return NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/slider", slider.Image);

            Helper.DeleteFile(path);

            //_context.sliders.Remove(slider);

            slider.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);

            if (slider is null) NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int id, Slider slider)
        {
            Slider dbSlider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m=>m.Id==id);
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View(dbSlider);
            if (!slider.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "The File Should Be Image Type");
                return View(dbSlider);
            } ;

            if (!slider.Photos.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "Please Upload Less Than 300KB");
                return View(dbSlider);
            }

            if (dbSlider == null) NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/slider", dbSlider.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photos.FileName;

            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/slider", fileName);

            await slider.Photos.SaveFiles(pathNew);


            slider.Image = fileName;
            _context.Sliders.Update(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int id)
        {
            Slider slider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m=>m.Id == id);
            return View(slider);
        }

        


    }
}
