using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class ServiceArea : BaseEntity
    {

        [Required,DataType(DataType.Text)]
        public string TeacherLevel { get; set; }
        [Required, DataType(DataType.Text)]
        public string Description { get; set; }

    }
}
