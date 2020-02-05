using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.BR.GeoHelper
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public static class GeoCoordinateHelper
    {
        public static double Distance(GeoCoordinate loc1, GeoCoordinate loc2, int type)
        {
            //1- miles, other km
            //Use 3960 if you want miles; use 6371 if you want km
            double R = (type == 1) ? 3960 : 6371;          // R is earth radius.
            double dLat = GeoCoordinateHelper.toRadian(loc2.Latitude - loc1.Latitude);
            double dLon = GeoCoordinateHelper.toRadian(loc2.Longitude - loc1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(GeoCoordinateHelper.toRadian(loc1.Latitude)) * Math.Cos(GeoCoordinateHelper.toRadian(loc2.Latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

        static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
