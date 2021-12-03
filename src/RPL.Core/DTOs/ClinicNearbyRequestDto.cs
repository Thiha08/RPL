using Microsoft.AspNetCore.Mvc;

namespace RPL.Core.DTOs
{
    public class ClinicNearbyRequestDto
    {
        [FromQuery]
        public double Latitude { get; set; }

        [FromQuery]
        public double Longitude { get; set; }

        [FromQuery]
        public string SearchString { get; set; }
    }
}
