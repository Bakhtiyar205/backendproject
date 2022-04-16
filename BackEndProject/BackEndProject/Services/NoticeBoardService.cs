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
    public class NoticeBoardService : INoticeBoardService
    {
        private readonly AppDbContext _context;
        public NoticeBoardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoticedEvent>> GetNotice()
        {
            List<NoticedEvent> noticedEvents = await _context.NoticedEvents
                                                    .Where(m => !m.IsDeleted)
                                                    .OrderByDescending(m => m.Date)
                                                    .ToListAsync();
            return noticedEvents;

        }
    }
}
