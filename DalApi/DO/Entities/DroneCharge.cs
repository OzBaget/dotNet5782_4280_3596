using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneCharge
    {
        public int Droneld { get; set; }
        public int Stationld { get; set; }
        public DateTime? PlugedIn { get; set; }
        public bool IsActived { get; set; }
        public override string ToString()
        {
            return
                $"Droneld:   {Droneld}\n" +
                $"Stationld: {Stationld}";
        }

    }
}
