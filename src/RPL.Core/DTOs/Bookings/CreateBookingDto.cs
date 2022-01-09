namespace RPL.Core.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public long ClinicId { get; set; }

        public long ScheduleId { get; set; }

        public long DoctorId { get; set; }


        public long PatientId { get; set; }


    }
}
