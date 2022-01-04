namespace RPL.Core.DTOs
{
    public class ClinicDto : BaseDto
    {
        public string ClinicName { get; set; }

        public AddressDto ClinicAddress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
