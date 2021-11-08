using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public Drone(string model, WeightCategories maxWeight)
            {
                Random r = new Random();
                Id = r.Next();
                Model = model;
                MaxWeight = maxWeight;

                Status = DroneStatuses.Available;
                Battery = 100;
            }
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return 
                    $"Drone ID:   {Id}\n" +
                    $"Modle:      {Model}\n" +
                    $"Max Weight: {MaxWeight}\n" +
                    $"Status:     {Status}\n" +
                    $"Battery:    {Battery}%";
            }
        }
    }
}
