using Microsoft.Extensions.Configuration;

namespace RPL.Core.Settings.SMS
{
    public class SmsSettings : ISmsSettings
    {
        private readonly IConfiguration _configuration;

        private bool _initialized = false;


        public string Url { get; set; }

        public string AuthorizationKey { get; set; }

        public string Sender { get; set; }

        public SmsSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize()
        {
            if (_initialized) return;

            Url = _configuration["SmsSettings:Url"];
            AuthorizationKey = _configuration["SmsSettings:AuthorizationKey"];
            Sender = _configuration["SmsSettings:Sender"];
        }
    }
}
