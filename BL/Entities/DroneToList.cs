using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public int Battery { get; set; }
        public DroneStatus Status { get; set; }
        public Location CurrentLocation { get; set; }
        public int ParcelId { get; set; }

        public override string ToString()
        {
            return            
                $"ID:                            {Id}\n" +
                $"Model:                         {Model}\n" +
                $"Max weight:                    {MaxWeight}\n" +
                $"Battery:                       {Battery}%\n" +
                $"Status:                        {Status}\n" +
                $"Current location:              {CurrentLocation}\n" +
                $"Parcel ID:                     {ParcelId}";
        }
    }
}
