using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
