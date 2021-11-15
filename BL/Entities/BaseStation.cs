using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public struct BaseStation
    {
        public int Id;
        public string Name;
        public Location Location;
        public int NumFreeChargers;
        public List<DroneInCharging> DronesInCharging;

    }
}
