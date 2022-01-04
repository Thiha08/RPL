using Ardalis.Specification;
using RPL.Core.Entities;
using RPL.Core.Filters;
using System.Linq;

namespace RPL.Core.Specifications.Doctors
{
    public class DoctorSchedulesByDateSpec : Specification<DoctorSchedule>
    {
        public DoctorSchedulesByDateSpec(DoctorScheduleFilter filter)
        {
            if (filter.DoctorIds.Any())
                Query.Where(x => filter.DoctorIds.Contains(x.DoctorId));

            if(filter.StartDateTime.HasValue)
                Query.Where(x => x.ScheduleStartDateTime >= filter.StartDateTime);

            if (filter.EndDateTime.HasValue)
                Query.Where(x => x.ScheduleEndDateTime <= filter.EndDateTime);
        }
    }
}
