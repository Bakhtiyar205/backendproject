using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class NoticedEvent : BaseEntity
    {
        [Required,DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required,DataType(DataType.Text),MinLength(11)]
        public string EvenetDetail { get; set; }
    }
}
