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
        public string Model;
        public WeightCategories MaxWeight;
        public double Battery;
        public DroneStatus Status;
        public ParcelInTransfer Parcel;
        public Location CurrentLocation;
    }
}
