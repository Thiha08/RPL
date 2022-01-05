using System.Threading.Tasks;

namespace RPL.Infrastructure.IntegrationServices.Interfaces
{
    public interface ISmsSender
    {
        Task SendSMSAsync(string to, string message);
    }
}
