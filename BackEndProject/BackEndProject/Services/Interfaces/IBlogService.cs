using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlog(int? take);
    }
}
