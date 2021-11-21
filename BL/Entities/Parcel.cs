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
                $"========Receiver=======================\n\t{Target.ToString().Replace("\n", "\n\t")}\n" +
                $"========Sender=========================\n\t{Sender.ToString().Replace("\n", "\n\t")}\n" +
                $"Weight status:                 {Weight}\n" +
                $"Prioritie status:              {Prioritie}\n";
            if (Drone.Id != 0)
                toString += $"========Drone==========================\n\t{Drone.ToString().Replace("\n", "\n\t")}\n";
                if (DateCreated != DateTime.MinValue)
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
