using System.Threading.Tasks;

namespace RPL.Infrastructure.IntegrationServices.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}
