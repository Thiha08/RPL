namespace RPL.Core.DTOs
{
    public class ClinicNearbyDto
    {
        public long DoctorScheduleId { get; set; }

        public string ClinicName { get; set; }

        public string DoctorName { get; set; }

        public string Address { get; set; }

        public string Schedule { get; set; }

        public bool IsBookingAvailable { get; set; }
    }
}
