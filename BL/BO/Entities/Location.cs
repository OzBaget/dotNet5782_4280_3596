using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {
            return coordsToSexag(Latitude,Longitude);
        }


        /// <summary>
        /// Convert decimal coords to sexagesimal coords
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <returns>deg°min'sec"E/W, deg°min'sec"E/W</returns>
        private string coordsToSexag(double lat, double lng)
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
        private string decToDegMinSec(double dec)
        {
            int deg = (int)dec;
            int min = (int)((dec - deg) * 60);
            double sec = (dec - deg - ((double)min / 60)) * 3600;
            return $"{deg}°{min}'{Math.Round(sec, 2)}\"";
        }

    }


}
