using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            public override string ToString()
            {
                return $"Droneld: {Droneld}\n" +
                    $"Stationld: {Stationld}\n";
            }

        }
    }
}
