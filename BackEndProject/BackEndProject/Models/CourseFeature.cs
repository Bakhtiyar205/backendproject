using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class CourseFeature : BaseEntity
    {
        [Required,DataType(DataType.DateTime)]
        public DateTime Starts { get; set; }
        [Required,DataType(DataType.Text)]
        public string Monthes { get; set; }
        [Required]
        public decimal Hours { get; set; }
        [Required, DataType(DataType.Text)]
        public string SkillLevel { get; set; }
        [Required, DataType(DataType.Text)]
        public string Language { get; set; }
        [Required]
        public int StundentCount { get; set; }
        [Required, DataType(DataType.Text)]
        public string Assesment { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
