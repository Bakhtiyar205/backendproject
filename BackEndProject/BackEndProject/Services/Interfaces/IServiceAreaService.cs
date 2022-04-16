using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface IServiceAreaService
    {
        Task<List<ServiceArea>> GetServiceArea();
    }
}
