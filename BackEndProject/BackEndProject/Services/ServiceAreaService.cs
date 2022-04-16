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
    public class ServiceAreaService : IServiceAreaService
    {
        private readonly AppDbContext _context;
        public ServiceAreaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceArea>> GetServiceArea()
        {
            List<ServiceArea> serviceAreas = await _context.ServiceAreas
                                                    .Where(m => !m.IsDeleted)
                                                    .OrderByDescending(m => m.Id)
                                                    .Take(3)
                                                    .ToListAsync();
            return serviceAreas;

        }
    }
}
