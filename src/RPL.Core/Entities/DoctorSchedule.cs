using RPL.SharedKernel;
using RPL.SharedKernel.Interfaces;
using System;

namespace RPL.Core.Entities
{
    public class DoctorSchedule : BaseEntity, IAggregateRoot
    {
        public long DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public DateTime ScheduleStartDateTime { get; set; }

        public DateTime ScheduleEndDateTime { get; set; }
    }
}
