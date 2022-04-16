using BackEndProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<AppUser> Subscription(string email,string name);
    }
}
