using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
