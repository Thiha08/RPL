using System;

namespace RPL.Core.Constants.Identity
{
    public static class IdentityConstants
    {
        public const string DefaultPassword = "welcome123";

        public static string GenerateVerificationCode
        {
            get
            {
                var random = new Random();
                var verificationCode = random.Next(1000, 999999).ToString("D6");
                return verificationCode;
            }
        }

        public const int VerificationCodeExpirySeconds = 300;

        public const string Ryawgen = "Ryawgen";

        public const string Paraman = "Paraman";

        public const string Larban = "Larban";
    }
}
