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
    public class AboutAreaService : IAboutAreaService
    {
        private readonly AppDbContext _context;
        public AboutAreaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AboutArea> GetAbout()
        {
            AboutArea aboutAreas = await _context.Abouts
                                    .Where(m => !m.IsDeleted)
                                    .OrderByDescending(m => m.Id)
                                    .FirstOrDefaultAsync();
            return aboutAreas;
        }
    }
}
