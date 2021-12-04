using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class VerificationCodeRequest
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
