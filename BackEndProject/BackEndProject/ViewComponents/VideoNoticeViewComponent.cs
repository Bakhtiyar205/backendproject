using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewComponents
{
    public class VideoNoticeViewComponent : ViewComponent
    {
        private readonly IVideoService _videoServie;
        public VideoNoticeViewComponent(IVideoService videoServie)
        {
            _videoServie = videoServie;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var videoService = await _videoServie.GetVideo();

            return await Task.FromResult(View(videoService));
        }
    }
}
