using Ardalis.Specification;
using RPL.Core.Entities;

namespace RPL.Core.ProjectAggregate.Specifications
{
    public class NearbyClinicsSearchSpec : Specification<Clinic>
    {
        public NearbyClinicsSearchSpec(double latitude, double longitude)
        {
            //Query
            //    .Where(clinic => new GeoCoordinate(clinic.ClinicAddress.Latitude, clinic.ClinicAddress.Longitude))

        }
    }
}
