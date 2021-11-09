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
        public CustomerInParcel Receiver;
        public Enums.DroneStatus DroneStatus;
        public Enums.Priorities Prioritie;
        public DroneInParcel Drone;
        public DateTime DateCreateParcel;
        public DateTime DateAttribution;
        public DateTime DateCollection;
        public DateTime Datesupply;
    }
}
