using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        public AboutController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email, string name)
        {
            var result = await _subscriptionService.Subscription(email,name);
            return RedirectToAction(nameof(Index));
        }
    }
}
