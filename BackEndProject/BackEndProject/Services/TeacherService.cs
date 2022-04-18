using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using BackEndProject.Utilities.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Paginate<TeacherSkill>> GetAll(int? take, int page = 1)
        {
            var newTake = take ?? 4;
            List<TeacherSkill> teachers = await _context.TeacherSkills
                                                .Include(m => m.Teacher)
                                                .Include(m => m.Skill)
                                                .Skip((page-1)*newTake)
                                                .Take(newTake)
                                                .Where(m=>!m.Teacher.IsDeleted)
                                                .ToListAsync();

            int totalPage =await GetPageCount(newTake);

            Paginate<TeacherSkill> resultTeachers = new Paginate<TeacherSkill>(teachers, page, totalPage);

            return resultTeachers;
        }

        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Teachers.Where(m=>!m.IsDeleted).CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }

        
    }
}
