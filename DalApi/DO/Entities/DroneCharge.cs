using System;

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
