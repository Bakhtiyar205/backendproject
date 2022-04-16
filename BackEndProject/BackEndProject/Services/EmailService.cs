using BackEndProject.Services.Interfaces;
using BackEndProject.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string emailTo, string userName, string html, string content)
        {
            var emailModel = _configuration.GetSection("EmailConfiguration").Get<EmailRequest>();
            var apiKey = emailModel.SecretKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailModel.SenderEmail, emailModel.SenderName);
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(emailTo, userName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, html);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
