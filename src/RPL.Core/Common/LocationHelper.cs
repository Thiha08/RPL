using RPL.Core.DTOs;
using RPL.Core.Extensions;
using System;

namespace RPL.Core.Common
{
    public static class LocationHelper
    {
        // Returns the great circle distance between two flats, as meters
        public static double DistanceBetweenPlaces(PlaceDto fromPlace, PlaceDto toPlace)
        {
            const int EarthRadius = 6371;

            double latitude = (toPlace.Latitude - fromPlace.Latitude).ToRadians();

            double longitude = (toPlace.Longitude - fromPlace.Longitude).ToRadians();

            double tmp = (Math.Sin(latitude / 2) * Math.Sin(latitude / 2)) +
                (Math.Cos(fromPlace.Latitude.ToRadians()) * Math.Cos(toPlace.Latitude.ToRadians()) *
                Math.Sin(longitude / 2) * Math.Sin(longitude / 2));

            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(tmp)));

            double d = EarthRadius * c;

            return d * 1000;
        }
    }
}
