using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Utilities.Files;
using BackEndProject.Utilities.Helpers;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]


    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeacherController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<TeacherSkill> teachersSkills = await _context.TeacherSkills
                                                        .Include(m => m.Teacher)
                                                        .Include(m => m.Skill)
                                                        .Where(m => !m.Teacher.IsDeleted)
                                                        .OrderByDescending(m => m.Id)
                                                        .ToListAsync();
            return View(teachersSkills);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(TeacherVM teacherVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!teacherVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "Only Image Type Is Acceptible");
                return View(teacherVM);
            }
            if (!teacherVM.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photos", "the File should be less than 300 KB");
                return View(teacherVM);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + teacherVM.Photo.FileName;

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/teacher", fileName);

            await teacherVM.Photo.SaveFiles(path);

            teacherVM.Teacher.Image = fileName;

            Teacher teacher = new Teacher
            {
                Image = teacherVM.Teacher.Image,
                Name = teacherVM.Teacher.Name,
                Knowledge = teacherVM.Teacher.Knowledge,
                AboutHeader = teacherVM.Teacher.AboutHeader,
                AboutDesc = teacherVM.Teacher.AboutDesc,
                Degree = teacherVM.Teacher.Degree,
                Experince = teacherVM.Teacher.Experince,
                Hobbies = teacherVM.Teacher.Hobbies,
                Faculty = teacherVM.Teacher.Faculty,
                Mail = teacherVM.Teacher.Mail,
                Phone = teacherVM.Teacher.Phone,
                Skype = teacherVM.Teacher.Skype,
                Pinterest = teacherVM.Teacher.Pinterest,
                Whatsapp = teacherVM.Teacher.Whatsapp,
                Vimeo = teacherVM.Teacher.Vimeo,
                Facebook = teacherVM.Teacher.Facebook,
                Twitter = teacherVM.Teacher.Twitter,
            };
            Skill skill = new Skill
            {
                Language = teacherVM.Skill.Language,
                TeamLeader = teacherVM.Skill.TeamLeader,
                Development = teacherVM.Skill.Development,
                Innovation = teacherVM.Skill.Innovation,
                Communication = teacherVM.Skill.Communication
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.Skills.AddAsync(skill);

            await _context.SaveChangesAsync();

            Teacher newTeacher = await _context.Teachers.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            Skill newSkill = await _context.Skills.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            TeacherSkill teacherSkill = new TeacherSkill
            {
                TeacherId = newTeacher.Id,
                SkillId = newSkill.Id
            };

            await _context.TeacherSkills.AddAsync(teacherSkill);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            TeacherSkill teacherSkill = await _context.TeacherSkills
                                            .Include(m => m.Teacher)
                                            .Include(m => m.Skill)
                                            .Where(m => m.TeacherId == id)
                                            .FirstOrDefaultAsync();
            


            return View(teacherSkill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TeacherVM teacherVM)
        {
            if (!ModelState.IsValid) return View();
            //if (ModelState["TeacherId"].ValidationState == ModelValidationState.Invalid) return View();
            //if (ModelState["SkillId"].ValidationState == ModelValidationState.Invalid) return View();


            TeacherSkill dbTeacherSkill = await _context.TeacherSkills
                                                .Include(m => m.Teacher)
                                                .Include(m => m.Skill)
                                                .Where(m => m.TeacherId == id)
                                                .FirstOrDefaultAsync();

            string path = Helper.GetFilePath(_environment.WebRootPath, "assets/img/teacher", dbTeacherSkill.Teacher.Image);

            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + teacherVM.Photo.FileName;
            string pathNew = Helper.GetFilePath(_environment.WebRootPath, "assets/img/teacher", fileName);
            await teacherVM.Photo.SaveFiles(pathNew);

            
            dbTeacherSkill.Teacher = teacherVM.Teacher;
            dbTeacherSkill.Skill = teacherVM.Skill;
            dbTeacherSkill.Teacher.Image = fileName;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            TeacherSkill teacherSkill = await _context.TeacherSkills
                                            .Include(m => m.Teacher)
                                            .Include(m => m.Teacher)
                                            .Where(m => m.TeacherId == id)
                                            .FirstOrDefaultAsync();

            teacherSkill.Teacher.IsDeleted = true;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }



    }
}
