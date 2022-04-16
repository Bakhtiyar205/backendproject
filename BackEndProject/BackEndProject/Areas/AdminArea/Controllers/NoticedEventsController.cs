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
    public class NoticedEventsController : Controller
    {
        private readonly AppDbContext _context;

        public NoticedEventsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<NoticedEvent> courseFeatures = await _context.NoticedEvents.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(courseFeatures);
        }

        public async Task<IActionResult> Detail(int id)
        {
            NoticedEvent noticedEvent = await _context.NoticedEvents.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (noticedEvent is null) NotFound();

            return View(noticedEvent);
        }

        public async Task<IActionResult> Edit(int id)
        {
            NoticedEvent noticedEvent = await _context.NoticedEvents.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (noticedEvent == null) return NotFound();

            return View(noticedEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NoticedEvent noticedEvent)
        {
            if (!ModelState.IsValid) return View();
            if (id != noticedEvent.Id) BadRequest();

            try
            {
                NoticedEvent dbNoticedEvent = await _context.NoticedEvents.FindAsync(id);

                dbNoticedEvent.Date = noticedEvent.Date;
                dbNoticedEvent.EvenetDetail = noticedEvent.EvenetDetail;
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
            NoticedEvent noticedEvent = await _context.NoticedEvents.FindAsync(id);
            if (noticedEvent is null) return View();
            noticedEvent.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoticedEvent noticedEvent)
        {
            if (!ModelState.IsValid) return View();

            await _context.NoticedEvents.AddAsync(noticedEvent);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}
