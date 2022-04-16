using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Skill : BaseEntity
    {
        public int Language { get; set; } = 0;
        public int TeamLeader { get; set; } = 0;
        public int Development { get; set; } = 0;
        public int Design { get; set; } = 0;
        public int Innovation { get; set; } = 0;
        public int Communication { get; set; } = 0;
        public ICollection<TeacherSkill> TeacherSkills { get; set; }
    }
}
