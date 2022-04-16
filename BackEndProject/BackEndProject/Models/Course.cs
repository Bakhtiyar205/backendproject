using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Course:BaseEntity
    {
        public string Image { get; set; }
        [Required, DataType(DataType.Text)]
        public string Header { get; set; }
        [Required, DataType(DataType.Text), MinLength(101)]
        public string Text { get; set; }
        [Required, DataType(DataType.Text)]
        public string HeaderAbout { get; set; }
        [Required, DataType(DataType.Text)]
        public string TextAbout { get; set; }
        [Required, DataType(DataType.Text)]
        public string HeaderCertification { get; set; }
        [Required, DataType(DataType.Text)]
        public string TextCertification { get; set; }
        [Required, DataType(DataType.Text)]
        public string HeadeReply { get; set; }
        [Required, DataType(DataType.Text)]
        public string TextReply { get; set; }
        public string CourseFeatureHeader { get; set; }
        [Required, DataType(DataType.Text)]
        public int CourseFee { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<CourseFeature> CourseFeature { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }



    }
}
