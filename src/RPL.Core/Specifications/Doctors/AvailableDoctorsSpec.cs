using Ardalis.Specification;
using RPL.Core.Entities;

namespace RPL.Core.Specifications.Doctors
{
    public class AvailableDoctorsSpec : Specification<Doctor>
    {
        public AvailableDoctorsSpec()
        {
            Query.Where(x => x.ClinicId == 0);
        }
    }
}
