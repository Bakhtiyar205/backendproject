using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class AboutArea : BaseEntity
    {
        [Required, DataType(DataType.Text)]
        public string Header { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text { get; set; }
        [Required, DataType(DataType.Text)]
        public string TextSecond { get; set; }
        [Required]
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}
