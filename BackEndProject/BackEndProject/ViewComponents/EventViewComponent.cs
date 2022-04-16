using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class EventViewComponent : ViewComponent
    {
        private readonly IEventDetailsService _eventDetailsService;
        public EventViewComponent(IEventDetailsService eventDetailsService)
        {
            _eventDetailsService = eventDetailsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? take)
        {
            var eventDetails = await _eventDetailsService.GetStudent(take);

            return await Task.FromResult(View(eventDetails));
        }
    }
}
