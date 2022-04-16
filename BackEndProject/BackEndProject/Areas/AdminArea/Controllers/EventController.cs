using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Utilities.Files;
using BackEndProject.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public EventController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }



        public async Task<EventDetail> GetElementById(int id)
        {
            return await _context.EventDetails.FindAsync(id);
        }

        public async Task<IActionResult> Index()
        {
            List<EventDetail> events = await _context.EventDetails.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(events);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDetail eventDetail)
        {
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid
                || ModelState["Text1"].ValidationState == ModelValidationState.Invalid
                || ModelState["Photo"].ValidationState == ModelValidationState.Invalid
                || ModelState["Text2"].ValidationState == ModelValidationState.Invalid
                || ModelState["Text3"].ValidationState == ModelValidationState.Invalid
                || ModelState["Address"].ValidationState == ModelValidationState.Invalid
                || ModelState["Time"].ValidationState == ModelValidationState.Invalid) return View();

            if (!eventDetail.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View(eventDetail);
            }
            if (!eventDetail.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View(eventDetail);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + eventDetail.Photo.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/event", fileName);

            await eventDetail.Photo.SaveFiles(path);

            eventDetail.Image = fileName;

            await _context.EventDetails.AddAsync(eventDetail);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            EventDetail eventDetail = await GetElementById(id);

            if (eventDetail == null) return NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/event", eventDetail.Image);

            Helper.DeleteFile(path);

            //_context.sliders.Remove(slider);

            eventDetail.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            EventDetail eventDetail = await _context.EventDetails.FindAsync(id);

            if (eventDetail is null) NotFound();

            return View(eventDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int id, EventDetail eventDetail)
        {
            EventDetail dbEvent = await _context.EventDetails.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View(dbEvent);
            if (!eventDetail.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "The File Should Be Image Type");
                return View(dbEvent);
            };

            if (!eventDetail.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "Please Upload Less Than 300KB");
                return View(dbEvent);
            }

            if (dbEvent == null) NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/event", dbEvent.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + eventDetail.Photo.FileName;

            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/event", fileName);

            await eventDetail.Photo.SaveFiles(pathNew);


            eventDetail.Image = fileName;
            _context.EventDetails.Update(eventDetail);

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
