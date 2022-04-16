using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Student:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<StudentImage> StudentImages { get; set; }
        public ICollection<StudentThought> StudentThought { get; set; }
    }
}
