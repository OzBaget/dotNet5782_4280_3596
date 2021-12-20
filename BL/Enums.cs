using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL 
{
    namespace BO
    {
        public enum WeightCategories { Light, Middle, Heavy }
        public enum DroneStatus { Available, UnderMaintenance, Delivery }
        public enum Priorities { Normal, Fast, Urgent }
        public enum ParcelStatus { Created, Scheduled, PickUp, Deliverd }
        public enum Permissions { Client, Administrator }

    }

}
