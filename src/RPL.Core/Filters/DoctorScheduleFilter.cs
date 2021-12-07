using System;
using System.Collections.Generic;

namespace RPL.Core.Filters
{
    public class DoctorScheduleFilter : BaseFilter
    {
        public List<long> DoctorIds { get; set; } = new List<long>();

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}
