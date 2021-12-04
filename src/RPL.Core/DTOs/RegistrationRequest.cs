using System;
using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class RegistrationRequest
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
