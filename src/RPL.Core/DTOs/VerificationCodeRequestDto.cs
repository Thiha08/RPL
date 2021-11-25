using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class VerificationCodeRequestDto
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
