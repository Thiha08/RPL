using Ardalis.Specification;
using RPL.Core.Entities;
using System.Linq;

namespace RPL.Core.Specifications.Bookings
{
    public class BookingInformationSpec : Specification<Booking>, ISingleResultSpecification
    {
        public BookingInformationSpec(long bookingId)
        {
            Query
                .Include(x => x.Clinic)
                .Include(x => x.Doctor)
                    .ThenInclude(y => y.DoctorSchedule)
                .Where(x => x.Id == bookingId && x.Doctor.DoctorSchedule.Any(y => y.Id == x.DoctorScheduleId));
               
        }
    }
}
