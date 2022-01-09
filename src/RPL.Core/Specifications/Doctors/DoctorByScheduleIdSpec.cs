using Ardalis.Specification;
using RPL.Core.Entities;
using System.Linq;

namespace RPL.Core.Specifications.Doctors
{
    public class DoctorByScheduleIdSpec : Specification<Doctor>, ISingleResultSpecification
    {
        public DoctorByScheduleIdSpec(long scheduleId)
        {
            Query
                .Include(x => x.DoctorSchedule)
                .Where(x => x.DoctorSchedule.Any(y => y.Id == scheduleId));
        }
    }
}
