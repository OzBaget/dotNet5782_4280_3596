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
                return $"ID: {Id}.\n" +
                    $"Name: {Name}\n" +
                    $"Phone Number: {Phone}\n" +
                    $"Position: {Lng},{Lat}";
            }
        }
    }
}
