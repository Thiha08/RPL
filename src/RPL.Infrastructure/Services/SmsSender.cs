using Newtonsoft.Json;
using RPL.Core.Interfaces;
using RPL.Core.Settings.SMS;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly ISmsSettings _smsSettings;

        public SmsSender(ISmsSettings smsSettings)
        {
            _smsSettings = smsSettings;
        }

        public async Task SendSMSAsync(string to, string message)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _smsSettings.AuthorizationKey);

            var result = await client.PostAsync($"{_smsSettings.Url}/v2/send",
                new StringContent(JsonConvert.SerializeObject(new { to, message, _smsSettings.Sender }), Encoding.UTF8, "application/json"));
        }
    }
}
