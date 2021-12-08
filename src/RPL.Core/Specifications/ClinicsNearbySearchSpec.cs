using Ardalis.Specification;
using RPL.Core.Common;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Filters;

namespace RPL.Core.Specifications
{
    public class ClinicsNearbySearchSpec : Specification<Clinic>
    {
        public ClinicsNearbySearchSpec(ClinicNearbyFilter filter)
        {
            if (filter.Latitude > 0 && filter.Longitude > 0)
                Query.Where(x =>
                    x.ClinicAddress.Latitude > 0 &&
                    x.ClinicAddress.Longitude > 0 &&
                    LocationHelper.DistanceBetweenPlaces(
                        new PlaceDto
                        {
                            Latitude = filter.Latitude,
                            Longitude = filter.Longitude
                        },
                        new PlaceDto
                        {
                            Latitude = x.ClinicAddress.Latitude,
                            Longitude = x.ClinicAddress.Longitude
                        }) <= filter.Radius);

            if (!string.IsNullOrEmpty(filter.Keyword))
                Query.Search(x => x.ClinicName, "%" + filter.Keyword + "%")
                     .Search(x => x.ClinicAddress.AddressBody, "%" + filter.Keyword + "%");

            //if (filter.IsPagingEnabled)
            //    Query.Skip(PaginationHelper.CalculateSkip(filter))
            //         .Take(PaginationHelper.CalculateTake(filter));

            //if (filter.LoadChildren)
            Query.Include(x => x.Doctors);
            //.ThenInclude(x => x.DoctorSchedule);

            //Query.OrderBy(x => x.ClinicName)
            //        .ThenByDescending(x => x.ClinicAddress.AddressBody);
        }
    }
}
