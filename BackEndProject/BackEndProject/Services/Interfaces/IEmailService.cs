using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string userName, string html, string content);
    }
}
