using System.Net.Mail;
using System.Net;
using WebAppHealthChecker.Application.Common;
using WebAppHealthChecker.Application.Common.Interfaces;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Infrastructure.NotificationService;

public class EmailService : INotificationService
{
    public void SendAsync(string reciver, string text, CancellationToken stoppingToken)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("email@mywebsitedomain.com");
        mailMessage.To.Add(reciver);
        mailMessage.Subject = "Subject";
        mailMessage.Body = text;

        SmtpClient smtpClient = new();
        smtpClient.Host = "smtp.mywebsitedomain.com";
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("username", "password");
        smtpClient.EnableSsl = true;

        smtpClient.SendAsync(mailMessage, stoppingToken);
    }
}