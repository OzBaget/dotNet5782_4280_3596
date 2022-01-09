using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneCharge
    {
        public DroneCharge(int droneld, int stationld)
        {
            Droneld = droneld;
            Stationld = stationld;
            PlugedIn = null;
        }

        public int Droneld { get; set; }
        public int Stationld { get; set; }
        public DateTime? PlugedIn { get; set; }
        public override string ToString()
        {
            return
                $"Droneld:   {Droneld}\n" +
                $"Stationld: {Stationld}";
        }

    }
}
