using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public struct DroneToList
    {
        public int Id;
        public string Model;
        public WeightCategories MaxWeight;
        public int Battery;
        public DroneStatus Status;
        public Location CurrentLocation;
        public int PacrelId;
    }
}
