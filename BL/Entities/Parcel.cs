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
                $"Weight status:                 {Weight}\n" +
                $"Prioritie status:              {Prioritie}\n";
                if (DateCreated != DateTime.MinValue)
                    toString += $"\nCreated date:                  {DateCreated}";
                if (DateScheduled != DateTime.MinValue)
                    toString += $"\nScheduled date:                {DateScheduled}";
                if (DatePickup != DateTime.MinValue)
                    toString += $"\nPick up date:                  {DatePickup}";
                if (DateDeliverd != DateTime.MinValue)
                    toString += $"\nDeliverd date:                 {DateDeliverd}";
                toString += $"        ===========Receiver============\n\t{Target.ToString().Replace("\n", "\n\t")}\n" +
                $"        ===========Sender==============\n\t{Sender.ToString().Replace("\n", "\n\t")}";
                if (Drone.Id != 0)
                    toString += $"\n        ===========Drone===============\n\t{Drone.ToString().Replace("\n", "\n\t")}";                
            return toString;
        }
    }
}
