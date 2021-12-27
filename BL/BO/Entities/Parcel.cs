using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {
        public int? Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Prioritie { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateScheduled { get; set; }
        public DateTime? DatePickup { get; set; }
        public DateTime? DateDeliverd { get; set; }
        public override string ToString()
        {

            string toString =
                $"ID:                            {Id}\n" +
                $"Weight status:                 {Weight}\n" +
                $"Prioritie status:              {Prioritie}";
                if (DateCreated != null)
                    toString += $"\nCreated date:                  {DateCreated}";
                if (DateScheduled != null)
                    toString += $"\nScheduled date:                {DateScheduled}";
                if (DatePickup != null)
                    toString += $"\nPick up date:                  {DatePickup}";
                if (DateDeliverd != null)
                    toString += $"\nDeliverd date:                 {DateDeliverd}";
                toString += $"\n        ===========Receiver============\n\t{Target.ToString().Replace("\n", "\n\t")}" +
                $"\n        ===========Sender==============\n\t{Sender.ToString().Replace("\n", "\n\t")}";
                if (Drone.Id != 0)
                    toString += $"\n        ===========Drone===============\n\t{Drone.ToString().Replace("\n", "\n\t")}";                
            return toString;
        }
    }
}
