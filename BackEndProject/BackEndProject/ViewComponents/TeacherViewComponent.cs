using BackEndProject.Datas;
using BackEndProject.Services.Interfaces;
using BackEndProject.Utilities.Pagination;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly ITeacherService _teacherService;
        public TeacherViewComponent(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? take,int page = 1)
        {
            

            var teacherService = await _teacherService.GetAll(take, page);

            TeacherViewModel teacherViewModel = new TeacherViewModel
            {
                TeacherSkil = teacherService,
                Take = take
            };

            return await Task.FromResult(View(teacherViewModel));
        }



        
    }
}
