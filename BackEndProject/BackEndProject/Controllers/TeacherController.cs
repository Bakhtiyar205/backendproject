using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    

    public class TeacherController : Controller
    {

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.page = page;
            return View();
        }
    }
}
