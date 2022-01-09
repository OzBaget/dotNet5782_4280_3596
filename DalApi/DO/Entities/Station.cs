using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int FreeChargeSlots { get; set; }
        public bool IsActived { get; set; }
        public override string ToString()
        {
            return
                $"Station Id:        {Id}\n" +
                $"Name:              {Name}\n" +
                $"Position:          {coordsToSexag(Lat, Lng)}\n" +
                $"Free Charge Slots: {FreeChargeSlots}";
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
        /// Convert Decimal degrees to degrees, minutes, seconds angle.
        /// </summary>
        /// <param name="dec">the degree</param>
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
