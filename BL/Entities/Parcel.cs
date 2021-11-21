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
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"ID:                            {Id}\n" +
                $"Sender:                        \n{Sender}" +
                $"Receiver:                      \n{Target}" +
                $"Weight status:                 {Weight}\n" +
                $"Prioritie status:              {Prioritie}\n";
                if (Drone.Id != 0)
                    toString += $"Parcel's Drone :               \n{Drone}";
                if(DateCreated != DateTime.MinValue)
                    toString += $"Created date:                  {DateCreated}\n";
                if (DateScheduled != DateTime.MinValue)
                    toString += $"Scheduled date:                {DateScheduled}\n";
                if (DatePickup != DateTime.MinValue)
                    toString += $"Pick up date:                  {DatePickup}\n";
                if (DateDeliverd!= DateTime.MinValue)
                    toString += $"Deliverd date:                 {DateDeliverd}\n";

            return toString;
        }
    }
}
