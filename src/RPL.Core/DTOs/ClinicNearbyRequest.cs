using Microsoft.AspNetCore.Mvc;

namespace RPL.Core.DTOs
{
    public class ClinicNearbyRequest
    {
        [FromQuery]
        public string Keyword { get; set; }

        [FromQuery]
        public double Latitude { get; set; }

        [FromQuery]
        public double Longitude { get; set; }

        [FromQuery]
        public int Radius { get; set; }
    }
}
