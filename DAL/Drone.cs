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
                switch (status)
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
    }
}
