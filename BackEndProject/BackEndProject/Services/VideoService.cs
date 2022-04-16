using BackEndProject.Datas;
using BackEndProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    public class VideoService : IVideoService
    {
        private readonly AppDbContext _context;
        public VideoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string,string>> GetVideo()
        {
            Dictionary<string, string> video = _context.Videos
                                                .AsEnumerable()
                                                .ToDictionary(m => m.Key, m => m.Value);
            return video;
        }
    }
}
