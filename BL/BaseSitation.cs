using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseSitation
    {
        public int Id;
        public string Name;
        public Location Locate;
        public int NumFreeChargers;
        public List<DroneInCharging> DronesInCharging;

    }
}
