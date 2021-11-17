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
            public Drone(int id,string model, WeightCategories maxWeight)
            {
                Id = id;
                Model = model;
                MaxWeight = maxWeight;

            }
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public override string ToString()
            {
                return $"Drone ID: {Id}\n" +
                    $"Modle: {Model}\n" +
                    $"Max Weight: {MaxWeight}\n";
            }
        }
    }
}
