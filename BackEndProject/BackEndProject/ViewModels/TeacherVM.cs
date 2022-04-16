using BackEndProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
    public class TeacherVM
    {
        public Teacher Teacher { get; set; }
        public Skill Skill { get; set; }
       [Required]
        public IFormFile Photo { get; set; }


    }
}
