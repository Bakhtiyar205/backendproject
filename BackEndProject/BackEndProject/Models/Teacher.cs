using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Teacher : BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Knowledge { get; set; }
        public string AboutHeader { get; set; }
        public string AboutDesc { get; set; }
        public string Degree { get; set; }
        public string Experince { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Whatsapp { get; set; }
        public string Vimeo { get; set; }
        public string Twitter { get; set; }
        public ICollection<TeacherSkill> TeacherSkills { get; set; }
    }
}
