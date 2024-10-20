using MimeKit;
using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
namespace EventManagement_Frontend.Services
{
    public class NotificationServices
    {
        public string Notification(string recipientName, string recipientEmail)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Event Management", "e18ca146.sdnbvc@gmail.com"));
            email.To.Add(new MailboxAddress(recipientName, recipientEmail));
            email.Subject = "Welcome to EventManagementApp!";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"Hello {recipientName},\n\nThank you for registering at EventManagementApp! We're excited to have you with us.\n\nBest Regards,\nThe EventManagementApp Team"
            };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("e18ca146.sdnbvc@gmail.com", "esuj gsxw gzrb ygoh");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    return "Email sent successfully.";
                }
                catch (Exception ex)
                {
                    return $"Email sending failed: {ex.Message}";
                }
            }
        }
    }
}
