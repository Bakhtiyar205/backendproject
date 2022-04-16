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
    public class ServiceAreaController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceAreaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<ServiceArea> services = await _context.ServiceAreas.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();
            return View(services);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ServiceArea service = await _context.ServiceAreas.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (service is null) NotFound();

            return View(service);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ServiceArea service = await _context.ServiceAreas.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceArea service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != service.Id) return BadRequest();

            try
            {
                ServiceArea dbService = await _context.ServiceAreas.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (dbService.TeacherLevel.ToLower().Trim() == service.TeacherLevel.ToLower().Trim() &&
                    dbService.Description.ToLower().Trim() == service.Description.ToLower().Trim())
                    return RedirectToAction(nameof(Index));

                _context.ServiceAreas.Update(service);

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
            ServiceArea service = await _context.ServiceAreas.FindAsync(id);

            if (service is null) NotFound();

            service.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceArea service)
        {
            if (!ModelState.IsValid) return View();

            await _context.ServiceAreas.AddAsync(service);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
