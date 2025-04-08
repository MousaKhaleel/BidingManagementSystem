using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Notifications
{
	public class NotificationService : INotificationService
	{//TODO
		public Task SendEmailAsync(string email, string subject, string body)
		{
			var mail = "sender@gmail.com";//TODO: change to your email
			var password = "password";

			var client = new SmtpClient("smtp-mail.gmail.com", 587)
			{
				Credentials = new NetworkCredential(mail, password),
				EnableSsl = true
			};
			var mailMessage = new MailMessage
			{
				From = new MailAddress(mail),
				Subject = subject,
				Body = body
			};
			mailMessage.To.Add(email);

			return client.SendMailAsync(mailMessage);
		}
	}
}
