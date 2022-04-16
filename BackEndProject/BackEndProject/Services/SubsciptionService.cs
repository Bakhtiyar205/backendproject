using BackEndProject.Datas;
using BackEndProject.Models;
using BackEndProject.Services.Interfaces;
using BackEndProject.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    
    public class SubsciptionService : ISubscriptionService
    {
        private readonly AppDbContext _context;
        public SubsciptionService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<AppUser> Subscription(string email,string name)
        {
            var subsciption = await _context.Users.FirstOrDefaultAsync(m => m.Email.ToLower().Trim() == email.ToLower().Trim()
                                                         && m.UserName.ToLower().Trim() == name.ToLower().Trim());

            subsciption.IsSubscribed = true;



            await _context.SaveChangesAsync();

            string content = "Email For Subscription";
            var mailRequest = new MailRequest
            {
                Subject = content,
                ToEmail = email,
            };

            return subsciption;
        }
    }
}
