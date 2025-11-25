
namespace OOP2.Domain.Services.Helper
{
    public static class HelperUser
    {
        public static bool IsWithin3KmOfSplit(double userLat, double userLon)
        {
            double splitLat = 43.5081;
            double splitLon = 16.4402;

            double distance = DistanceInKm(userLat, userLon, splitLat, splitLon);

            return distance <= 3.0;
        }
        public static double DistanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371;
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
