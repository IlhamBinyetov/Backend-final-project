
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;

namespace QuarterTemplate.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {


        public void Send(string to, string subject, string html)
        {
            //// create message
            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("binyetov631@gmail.com"));
            //email.To.Add(MailboxAddress.Parse(to));
            //email.Subject = subject;
            //email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("binyetov631@gmail.com", "7276522i"),
            //    EnableSsl = true
            //};
            //client.Send("binyetov631@gmail.com", "muradheyderov@gmail.com", "test", "testbody");



            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("binyetov631@gmail.com");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = html;
                mail.IsBodyHtml = true;
        

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("binyetov631@gmail.com", "xyz");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
