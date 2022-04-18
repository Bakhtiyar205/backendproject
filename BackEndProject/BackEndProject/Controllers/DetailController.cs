using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;

        public DetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CourseDetail()
        {
            List<Category> category = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();
            Course course = await _context.Courses.Include(m => m.CourseFeature).FirstOrDefaultAsync();

            CourseVM courseVM = new CourseVM
            {
                Categories = category,
                Course = course
            };

            return View(courseVM);
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            Blog blog = await _context.Blog.Where(m => m.Id == id).FirstOrDefaultAsync();
            return View(blog);
        }
        public async Task<IActionResult> EventDetailView(int id)
        {
            EventDetail eventDetail = await _context.EventDetails
                                            .Include(m => m.Speakers)
                                            .Where(m => m.Id == id)
                                            .FirstOrDefaultAsync();
            List<Speakers> speakers = await _context.Speakers.ToListAsync();


            EventDetailVM events = new EventDetailVM
            {
                Events = eventDetail,
                Speakers = speakers
                
            };

            return View(events);
        }

        public async Task<IActionResult> TeacherDetail(int id)
        {
            TeacherSkill teacher = await _context.TeacherSkills
                                                 .Include(m => m.Teacher)
                                                 .Include(m => m.Skill)
                                                 .Where(m => !m.Teacher.IsDeleted && m.TeacherId == id)
                                                 .FirstOrDefaultAsync();
            return View(teacher);
        }

        public async Task<IActionResult> CourseSearch(string search)
        {
            List<Course> courses = await _context.Courses.ToListAsync();
            List<Course> courseResults = new List<Course> { };
            foreach (var course in courses)
            {
                if (course.Header.ToLower().Trim().Contains(search.ToLower().Trim()))
                {
                    courseResults.Add(course);
                }

            }
            
            return View(courseResults);
        }
    }
}
