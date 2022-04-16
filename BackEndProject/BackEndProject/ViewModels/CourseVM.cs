using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
    public class CourseVM
    {
        public List<Category> Categories { get; set; }
        public Course Course { get; set; }

    }
}
