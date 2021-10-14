using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double lng { get; set; }
            public double lat { get; set; }

            public override string ToString()
            {
                return $"ID: {Id}.\n" +
                    $"Name: {Name}\n" +
                    $"Phone Number: {Phone}\n" +
                    $"Position: {lng},{lat}";
            }
        }
        public struct DroneCharge 
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
            public override string ToString()
            {
                return $"Droneld: {Droneld}.\n" +
                    $"Stationld: {Stationld}\n";
            }

        }
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Lng { get; set; }
            public double Lat { get; set; }
            public int ChargeSlots { get; set; }
            public override string ToString()
            {
                return $"Station Id: {Id}.\n" +
                    $"Name: {Name}\n"+
                   $"Position: {Lng},{Lat}"+
                    $"ChargeSlots: {ChargeSlots}.\n";
            }

        }
        public struct Drone 
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public Weight
        }
    }
}
