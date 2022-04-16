using BackEndProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Datas
{

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }


        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ServiceArea> ServiceAreas { get; set; }
        public DbSet<AboutArea> Abouts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<NoticedEvent> NoticedEvents { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<Speakers> Speakers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentImage> StudentImages { get; set; }
        public DbSet<StudentThought> StudentThoughts { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}
