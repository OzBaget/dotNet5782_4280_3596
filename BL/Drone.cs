using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public int Id;
        public string Modle;
        public Enums.WeightCategories Weight;
        public double Battery;
        public Enums.DroneStatus Status;
        public Location CurrentLocation;
        public int PacrelId;
    }
}
