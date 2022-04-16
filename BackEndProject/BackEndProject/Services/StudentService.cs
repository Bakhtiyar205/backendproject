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
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudent()
        {
            List<Student> students = await _context.Students
                                            .Include(m => m.StudentImages)
                                            .Include(m => m.StudentThought)
                                            .Where(m => !m.IsDeleted)
                                            .OrderByDescending(m => m.Id)
                                            .ToListAsync();
            return students;
        }
    }
}
