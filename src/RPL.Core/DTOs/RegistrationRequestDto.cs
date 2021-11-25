using System;
using System.ComponentModel.DataAnnotations;

namespace RPL.Core.DTOs
{
    public class RegistrationRequestDto
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }


        public string Address { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
