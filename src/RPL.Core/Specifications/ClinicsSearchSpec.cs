using Ardalis.Specification;
using RPL.Core.Entities;
using System.Linq;

namespace RPL.Core.Specifications
{
    public class ClinicsSearchSpec : Specification<Clinic>
    {
        public ClinicsSearchSpec(string searchString)
        {
            Query
                .Where(clinic => clinic.ClinicName.Contains(searchString) ||
                clinic.ClinicAddress.AddressBody.Contains(searchString));
        }
    }
}
