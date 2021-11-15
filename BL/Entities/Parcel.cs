using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id;
        public CustomerInParcel Sender;
        public CustomerInParcel Target;
        public WeightCategories Weight;
        public Priorities Prioritie;
        public DroneInParcel Drone;
        public DateTime DateCreated;
        public DateTime DateScheduled;
        public DateTime DatePickup;
        public DateTime DateDeliverd;
    }
}
