using System.Collections.Generic;

namespace BO
{
    public class BaseStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int NumFreeChargers { get; set; }
        public List<DroneInCharging> DronesInCharging { get; set; }

        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Location:                      {Location}\n" +
                $"Free chargers:                 {NumFreeChargers}";
            if (DronesInCharging.Count > 0)
            {
                toString += $"\n - Drones in charging:";
                int count = 1;
                foreach (var droneInChargeing in DronesInCharging)
                {
                    toString += $"\n\t===========Drone #{count}===========\n\t{droneInChargeing.ToString().Replace("\n", "\n\t")}";
                    ++count;

                }
            }
            return toString;

        }
    }
}
