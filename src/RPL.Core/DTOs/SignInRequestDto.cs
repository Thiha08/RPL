using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class SignInRequestDto
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
