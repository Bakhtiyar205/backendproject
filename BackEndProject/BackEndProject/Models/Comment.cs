using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CommentSection { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
