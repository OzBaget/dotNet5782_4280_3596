using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public struct Parcel
    {
        public int Id;
        public CustomerInParcel Sender;
        public CustomerInParcel Receiver;
        public WeightCategories Weight;
        public Priorities Prioritie;
        public DroneInParcel Drone;
        public DateTime DateCreated;
        public DateTime DateScheduled;
        public DateTime DatePickup;
        public DateTime DateDeliverd;
    }
}
