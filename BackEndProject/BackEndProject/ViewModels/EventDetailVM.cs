using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.ViewModels
{
    public class EventDetailVM
    {
        public EventDetail Events { get; set; }
        public List<Speakers> Speakers { get; set; }
    }
}
