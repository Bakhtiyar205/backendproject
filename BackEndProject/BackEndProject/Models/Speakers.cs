using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Models
{
    public class Speakers:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Duty { get; set; }
        public string Company { get; set; }
        public int EventDetailId { get; set; }
        public EventDetail EventDetail { get; set; }
    }
}
