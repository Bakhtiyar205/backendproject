using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService, AppDbContext context)
        {
            _context = context;
            _courseService = courseService;
        }

        public async  Task<IActionResult> Index(int? categoryId)
        {
            return View(categoryId);
        }

        public async Task<IActionResult> CourseDetail(int id)
        {
            Course course = await _context.Courses.Include(m => m.CourseFeature).Where(m => m.Id == id).FirstOrDefaultAsync();

            return View(course);
        }
    }
}
