using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class VerificationRequest
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }
}
