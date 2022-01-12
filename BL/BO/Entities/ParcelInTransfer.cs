using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInTransfer
    {
        public int Id { get; set; }
        public bool IsInTransfer { get; set; } = true;
        public Priorities Prioritie { get; set; } = new();
        public WeightCategories Weight { get; set; } = new();
        public CustomerInParcel Sender { get; set; } = new();
        public CustomerInParcel Target { get; set; } = new();
        public Location PickupLocation { get; set; } = new();
        public Location TargetLocation { get; set; } = new();
        public double Distance { get; set; }
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Is in transer:                 {IsInTransfer}\n" +
                $"Prioritie status:              {Prioritie}\n" +
                $"Weight status:                 {Weight}\n" +
                $"        ==========Receiver=============\n\t{Target.ToString().Replace("\n", "\n\t")}\n" +
                $"        ==========Sender===============\n\t{Sender.ToString().Replace("\n", "\n\t")}\n" +                
                $"Pick up location:              {PickupLocation}\n" +
                $"Target location:               {TargetLocation}\n" +
                $"Distance frome target:         {Distance}";
                

            return toString;
        }
    }
}
