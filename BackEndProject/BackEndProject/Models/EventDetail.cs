using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class EventDetail:BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required, DataType(DataType.Text)]
        public string Name { get; set; }
        [Required, DataType(DataType.Text)]
        public string Time { get; set; }
        [Required, DataType(DataType.Text)]
        public string Address { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text1 { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text2 { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text3 { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public ICollection<Speakers> Speakers { get; set; }
    }
}
