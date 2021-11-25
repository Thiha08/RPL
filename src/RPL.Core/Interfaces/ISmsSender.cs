using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface ISmsSender
    {
        Task SendSMSAsync(string to, string message);
    }
}
