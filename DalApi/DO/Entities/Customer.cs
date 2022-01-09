using System;


namespace DO
{

    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsActived { get; set; }

        public override string ToString()
        {
            return
                $"Customer ID:  {Id}\n" +
                $"Name:         {Name}\n" +
                $"Phone Number: {Phone}\n" +
                $"Position:     {coordsToSexag(Lat, Lng)}\n";
        }


        /// <summary>
        /// Convert decimal coords to sexagesimal coords
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <returns>deg°min'sec"E/W, deg°min'sec"E/W</returns>
        private static string coordsToSexag(double lat, double lng)
        {
            char signLng = lng < 0 ? 'W' : 'E';
            char signLat = lat < 0 ? 'S' : 'N';

            return $"{decToDegMinSec(Math.Abs(lat))}{signLat}, {decToDegMinSec(Math.Abs(lng))}{signLng}";
        }

        /// <summary>
        /// Convert decimal degrees to degrees, minutes, seconds angle.
        /// </summary>
        /// <param name="dec">the degree in decimal</param>
        /// <returns>deg°min'sec"</returns>
        private static string decToDegMinSec(double dec)
        {
            int deg = (int)dec;
            int min = (int)((dec - deg) * 60);
            double sec = (dec - deg - ((double)min / 60)) * 3600;
            return $"{deg}°{min}'{Math.Round(sec, 3)}\"";
        }
    }
}

