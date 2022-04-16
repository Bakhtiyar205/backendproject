using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class NoticeViewComponent : ViewComponent
    {
        private readonly INoticeBoardService _noticeBoardService;
        public NoticeViewComponent(INoticeBoardService noticeBoardService)
        {
            _noticeBoardService = noticeBoardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notice = await _noticeBoardService.GetNotice();

            return await Task.FromResult(View(notice));
        }
    }
}
