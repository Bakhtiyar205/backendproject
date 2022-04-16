using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class ServiceAreaViewComponent : ViewComponent
    {
        private readonly IServiceAreaService _serviceAreaService;
        public ServiceAreaViewComponent(IServiceAreaService serviceAreaService)
        {
            _serviceAreaService = serviceAreaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderService = await _serviceAreaService.GetServiceArea();

            return await Task.FromResult(View(sliderService));
        }
    }
}
