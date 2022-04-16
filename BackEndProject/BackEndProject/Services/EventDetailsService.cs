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
    public class EventDetailsService : IEventDetailsService
    {
        private readonly AppDbContext _context;
        public EventDetailsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventDetail>> GetStudent(int? take)
        {
            var newTake = take ?? _context.EventDetails.Count();
            List<EventDetail> eventDetails = await _context.EventDetails
                                                    .Include(m => m.Speakers)
                                                    .Where(m => !m.IsDeleted)
                                                    .OrderByDescending(m => m.Id)
                                                    .Take(newTake)
                                                    .ToListAsync();
            return eventDetails;
        }
    }
}
