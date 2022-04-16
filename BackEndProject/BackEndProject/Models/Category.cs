using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Category:BaseEntity
    {
        [Required,DataType(DataType.Text)]
        public string CategorySection { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
