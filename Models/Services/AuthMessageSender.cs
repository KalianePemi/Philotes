using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace AspCore_Email.Services
{
    public class AuthMessageSender //: IEmailSender
    {        

        public Task SendEmailAsync(EmailSettings email)
        {
            try
            {
                Execute(email).Wait();
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Execute(EmailSettings email)
        {
            try
            {
                string toEmail = email.ToEmail;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(email.UsernameEmail, "Kaliane Miranda")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(email.CcEmail));

                mail.Subject = " : ) " + email.Subject;
                mail.Body = email.Message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(email.PrimaryDomain, email.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(email.UsernameEmail, email.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}