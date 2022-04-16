using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourse(int? take, int? categoryId);
    }
}
