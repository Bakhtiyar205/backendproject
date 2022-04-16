using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    public class CourseService :ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetCourse(int? take,int? categoryId)
        {
            var newTake = take ?? _context.Courses.Count();
            List<Course> courses = null;
            if (categoryId is null || categoryId ==0)
            {
                courses = await _context.Courses
                                        .Include(m => m.CourseFeature)
                                        .Where(m => !m.IsDeleted)
                                        .Take(newTake)
                                        .OrderByDescending(m => m.Id)
                                        .ToListAsync();
            }
            else
            {
                courses = await _context.Courses
                                        .Include(m => m.CourseFeature)
                                        .Where(m => !m.IsDeleted && m.CategoryId == categoryId)
                                        .Take(newTake)
                                        .OrderByDescending(m => m.Id)
                                        .ToListAsync();
            }

         
            return courses;
        }
        
    }
}
