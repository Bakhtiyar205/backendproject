using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required, DataType(DataType.Text)]
        public string Header { get; set; }
        [Required, DataType(DataType.Text)]
        public string InnerText { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }
    }
}
