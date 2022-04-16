using BackEndProject.Datas;
using BackEndProject.Services;
using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    

    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutAreaService _aboutService;
        public AboutViewComponent(IAboutAreaService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutService = await _aboutService.GetAbout();

            return await Task.FromResult(View(aboutService));
        }
    }
}
