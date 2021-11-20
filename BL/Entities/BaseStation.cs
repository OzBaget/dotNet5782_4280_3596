using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation
    {
        public int Id;
        public string Name;
        public Location Location;
        public int NumFreeChargers;
        public List<DroneInCharging> DronesInCharging;

        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Location:                      {Location}\n" +
                $"Free chargers:                 {NumFreeChargers}\n" +
                $"Drones in charging:";
            foreach (var droneInChargeing in DronesInCharging)
            {
                toString += "\n=============\n" + droneInChargeing.ToString();
            }
            return toString;
                
        }
    }
}
