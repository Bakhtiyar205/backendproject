using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<ServiceArea> ServiceAreas { get; set; }
        public AboutArea AboutArea { get; set; }
        public List<Course> Courses { get; set; }
        public Dictionary<string,string> Video { get; set; }
        public List<NoticedEvent> NoticedEvents { get; set; }
        public List<EventDetail> EventDetails { get; set; }
        public List<Student> Students { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
