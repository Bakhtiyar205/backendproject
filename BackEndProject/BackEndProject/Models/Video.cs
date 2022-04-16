using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Video 
    {
        public int Id { get; set; }
        public string Key { get; set; }
        [Required,DataType(DataType.Text)]
        public string Value { get; set; }
    }
}
