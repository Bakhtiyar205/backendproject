using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface IVideoService
    {
        Task<Dictionary<string, string>> GetVideo();
    }
}
