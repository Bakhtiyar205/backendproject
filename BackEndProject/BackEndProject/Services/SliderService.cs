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
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetSlider()
        {
            List<Slider> sliders = await _context.Sliders
                                          .Where(m => !m.IsDeleted)
                                          .OrderByDescending(m => m.Id)
                                          .Take(3)
                                          .ToListAsync();
            return sliders;
        }
    }
}
