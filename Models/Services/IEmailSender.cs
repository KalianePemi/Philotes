using System.Threading.Tasks;
namespace AspCore_Email.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}