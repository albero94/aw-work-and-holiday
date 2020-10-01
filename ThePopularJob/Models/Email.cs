using MailKit.Net.Smtp;
using MimeKit;

namespace ThePopularJob.Models
{
    public class Email
    {
        public void SendEmail()
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Name ", "...@thepopularjob.com"));
            message.To.Add(new MailboxAddress("Name", "...@gmail.com"));
            message.To.Add(new MailboxAddress("Name", "...@gmail.com"));
            message.To.Add(new MailboxAddress("Name", "...@gmail.com"));
            message.Subject = "Test email from ThePopularJob using .NET and Hostgator mail server";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Guys,
I'm sending this email from a program :)
But as you can see it appears as not verified or something like that, I will dig more into that.

Alvaro"
            };

            using (var client = new SmtpClient())
            {
                // server information in hostgator
                client.Connect("....hostgator.com", 465);
                client.Authenticate("...@thepopularjob.com", "password!");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
