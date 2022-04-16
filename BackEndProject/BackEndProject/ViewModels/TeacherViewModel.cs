using BackEndProject.Models;
using BackEndProject.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
    public class TeacherViewModel
    {
        public Paginate<TeacherSkill> TeacherSkil { get; set; }
        public int? Take { get; set; }
        public int Page { get; set; }
    }
}
