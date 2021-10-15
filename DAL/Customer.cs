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
    }
}
