using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            List<Comment> comments = await _commentService.GetComments();
            return View(comments);
        }
        public async Task<IActionResult> Aprove(int id)
        {
            await _commentService.ApproveComment(id);

            return RedirectToAction(nameof(Index));
                
        }

        public async Task<IActionResult> DisAprove(int id)
        {
            await _commentService.DisApproveComment(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
