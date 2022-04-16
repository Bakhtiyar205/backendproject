using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class StudentThought:BaseEntity
    {
        public string Thought { get; set; }
        public bool IsMain { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
