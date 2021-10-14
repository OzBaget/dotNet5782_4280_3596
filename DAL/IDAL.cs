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
                return $"Customer ID: {Id}.\n" +
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
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {

                return $"Drone ID: {Id}.\n" +
                    $"Modle: {Model}\n" +
                    $"Max Weight: {MaxWeight}\n" +
                    $"Status :{Status}" +
                    $"Battery Mode: {Battery}";
            }
    }
        public struct Parcel
        {
            int Id { get; set; }
            int SenderId { get; set; }
            int TargetId { get; set; }
            WeightCategories Weight { get; set; }
            Priorities Priority { get; set; }
            DateTime Requsted { get; set; }
            int DroneId { get; set; } //TODO: default: 0
            DateTime Scheduled { get; set; }
            DateTime PickedUp { get; set; }
            DateTime Delivered { get; set; }

            public override string ToString()
            {
                return $"Parcel ID:{Id}\n Sender ID:";
            }
        }
            
        public enum WeightCategories { Light, Middle, Heavy }
        public enum DroneStatuses { available, UnderMaintenance, delivery  }
        public enum Priorities { Normal, Fast, Urgent }
    }
}
