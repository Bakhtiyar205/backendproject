using BackEndProject.Models;
using BackEndProject.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Paginate<TeacherSkill>> GetAll(int? take, int page = 1);
    }
}
