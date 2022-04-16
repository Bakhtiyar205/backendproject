using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class StudentViewComponent : ViewComponent
    {
        private readonly IStudentService _studentService;
        public StudentViewComponent(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var studentService = await _studentService.GetStudent();

            return await Task.FromResult(View(studentService));
        }
    }
}
