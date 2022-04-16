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
    public class VideoController : Controller
    {
        private readonly AppDbContext _context;
        public VideoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            Video video = await _context.Videos.FirstOrDefaultAsync();
            return View(video);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Video video = await _context.Videos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (video == null) return NotFound();

            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Video video)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != video.Id) return BadRequest();

            try
            {
                Video dbVideo = await _context.Videos.FirstOrDefaultAsync(m => m.Id == id);
                if (dbVideo.Value.ToLower().Trim() == video.Value.ToLower().Trim())
                    return RedirectToAction(nameof(Index));

                dbVideo.Value = video.Value;

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
