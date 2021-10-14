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
            public double Lng { get; set; }
            public double Lat { get; set; }

            public override string ToString()
            {
                return $"Customer ID: {Id}.\n" +
                    $"Name: {Name}\n" +
                    $"Phone Number: {Phone}\n" +
                    $"Position: {Lng},{Lat}";
            }
        }
        public struct Parcel
        {
            int Id { get; set; }
            int SenderId { get; set; }
            int TargetId { get; set; }
            WhightCategories Whight { get; set; }
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

        public enum WhightCategories { Light, Middle, Heavy }
        public enum Priorities { Normal, Fast, Urgent }
    }
}
