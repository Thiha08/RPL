using RPL.Core.Constants;
using RPL.SharedKernel;
using RPL.SharedKernel.Interfaces;
using System;

namespace RPL.Core.Entities
{
    public class Booking : BaseEntity, IAggregateRoot
    {
        public int BookingNumber { get; set; }

        public string Description { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public DateTime? BookingStartDateTime { get; set; }

        public DateTime? BookingEndDateTime { get; set; }

        public DateTime? BookingCancelledDateTime { get; set; }

        public long PatientId { get; set; }

        public Patient Patient { get; set; }

        public long DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public long ClinicId { get; set; }

        public Clinic Clinic { get; set; }

    }
}
