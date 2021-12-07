using System;

namespace RPL.Core.DTOs
{
    public class ClinicNearbyDto
    {
        public long Id { get; set; }

        public string ClinicName { get; set; }

        public string DoctorName { get; set; }

        public string Address { get; set; }

        public string Schedule { get; set; }

        public DateTime ScheduleStartDateTime { get; set; }

        public DateTime ScheduleEndDateTime { get; set; }
    }
}
