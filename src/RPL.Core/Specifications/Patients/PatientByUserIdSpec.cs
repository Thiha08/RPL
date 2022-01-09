using Ardalis.Specification;
using RPL.Core.Entities;

namespace RPL.Core.Specifications.Patients
{
    public class PatientByUserIdSpec : Specification<Patient>, ISingleResultSpecification
    {
        public PatientByUserIdSpec(string userId)
        {
            Query
                .Where(x => x.UserId == userId);
        }
    }
}
