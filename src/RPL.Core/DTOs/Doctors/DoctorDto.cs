using System;

namespace RPL.Core.DTOs.Doctors
{
    public class DoctorDto : BaseDto
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public AddressDto Address { get; set; }

        
    }
}
