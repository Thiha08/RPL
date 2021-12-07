namespace RPL.Core.Filters
{
    public class ClinicNearbyFilter : BaseFilter
    {
        public string Keyword { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Radius { get; set; }
    }
}
