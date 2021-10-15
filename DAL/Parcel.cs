using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
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
            private static string WeightToStr(IDAL.DO.WeightCategories weight)
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
    }
}
