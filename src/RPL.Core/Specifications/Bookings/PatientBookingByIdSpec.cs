using Ardalis.Specification;
using RPL.Core.Entities;

namespace RPL.Core.Specifications.Bookings
{
    public class PatientBookingByIdSpec : Specification<Booking>, ISingleResultSpecification
    {
        public PatientBookingByIdSpec(long patientId, long bookingId)
        {
            Query
                .Where(x => x.Id == bookingId && x.PatientId == patientId);
        }
    }
}
