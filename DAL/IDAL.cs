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
                return $"Customer ID: {Id}\n" +
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
                return $"Droneld: {Droneld}\n" +
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
                return $"Station Id: {Id}\n" +
                    $"Name: {Name}\n"+
                   $"Position: {Lng},{Lat}\n"+
                    $"ChargeSlots: {ChargeSlots}";
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
                    $"Max Weight: {WeightToStr(MaxWeight)}\n" +
                    $"Status :{StatusToStr(Status)}" +
                    $"Battery : {Battery}%";
            }
            private static string WeightToStr(WeightCategories weight)
            {
                switch (weight)
                {
                    case WeightCategories.Light:
                        return "Light";
                    case WeightCategories.Middle:
                        return "Middle";
                    case WeightCategories.Heavy:
                        return "Heavy";
                    default:
                        return "ERROR";
                }
            }
            private static string StatusToStr(DroneStatuses status)
            {
                switch(status)
                {
                    case DroneStatuses.Available:
                        return "Available";
                    case DroneStatuses.UnderMaintenance:
                        return "Under Maintenance";
                    case DroneStatuses.Delivery:
                        return "Delivery";
                    default:
                        return "ERROR";
                }
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
                return $"Parcel ID: {Id}\n" +
                    $"Sender ID: {SenderId}\n" +
                    $"Target ID:{TargetId}\n" +
                    $"Weight: {WeightToStr(Weight)}\n" +
                    $"Priority: {PriortyToStr(Priority)}\n" +
                    $"Assigned Drone ID: {DroneId}\n" +
                    $"Requsted Time: {Requsted.ToString("dd/MM/yyyy HH:mm:ss")}" +
                    $"Scheduled Time: {Scheduled.ToString("dd/MM/yyyy HH:mm:ss")}" +
                    $"PickedUp Time: {PickedUp.ToString("dd/MM/yyyy HH:mm:ss")}" +
                    $"Delivered Time: {Delivered.ToString("dd/MM/yyyy HH:mm:ss")}";
            }
            private static string WeightToStr(WeightCategories weight)
            {
                switch (weight)
                {
                    case WeightCategories.Light:
                        return "Light";
                    case WeightCategories.Middle:
                        return "Middle";
                    case WeightCategories.Heavy:
                        return "Heavy";
                    default:
                        return "ERROR";
                }
            }
            private static string PriortyToStr(Priorities priority)
            {
                switch (priority)
                {
                    case Priorities.Normal:
                        return "Normal";
                       
                    case Priorities.Fast:
                        return "Fast";
                    case Priorities.Urgent:
                        return "Urgent";
                    default:
                        return "ERROR";
                }
            }
        }
            
        public enum WeightCategories { Light, Middle, Heavy }
        public enum DroneStatuses {Available, UnderMaintenance, Delivery  }
        public enum Priorities { Normal, Fast, Urgent }
    }
}
