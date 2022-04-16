using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Utilities.Files;
using BackEndProject.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public BlogController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<Blog> GetElementById(int id)
        {
            return await _context.Blog.FindAsync(id);
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> courses = await _context.Blog.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();

            return View(courses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Header"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Text"].ValidationState == ModelValidationState.Invalid) return View();
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid) return View();


            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View(blog);
            }
            if (!blog.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View(blog);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/blog", fileName);

            await blog.Photo.SaveFiles(path);

            blog.Image = fileName;

            await _context.Blog.AddAsync(blog);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await GetElementById(id);

            if (blog == null) return NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/blog", blog.Image);

            Helper.DeleteFile(path);

            //_context.sliders.Remove(slider);

            blog.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            Blog blog = await _context.Blog.FindAsync(id);

            if (blog is null) NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int id, Blog blog)
        {
            Blog dbBlog = await _context.Blog.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View(dbBlog);
            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "The File Should Be Image Type");
                return View(dbBlog);
            };

            if (!blog.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "Please Upload Less Than 300KB");
                return View(dbBlog);
            }

            if (dbBlog == null) NotFound();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/blog", dbBlog.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/blog", fileName);

            await blog.Photo.SaveFiles(pathNew);


            blog.Image = fileName;
            _context.Blog.Update(blog);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int id)
        {
            Blog blog = await _context.Blog.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return View(blog);
        }
    }
}
