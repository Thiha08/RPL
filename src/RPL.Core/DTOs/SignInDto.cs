namespace RPL.Core.DTOs
{
    public class SignInDto
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }
    }
}
