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
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlog(int? take)
        {
            var newTake = take ?? _context.Blog.Count();
            List<Blog> blogs = await _context.Blog
                                    .Where(m => !m.IsDeleted)
                                    .OrderByDescending(m => m.Date)
                                    .Take(newTake)
                                    .ToListAsync();
            return blogs;
        }
    }
}
