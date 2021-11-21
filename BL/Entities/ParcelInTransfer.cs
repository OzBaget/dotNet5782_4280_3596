using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInTransfer
    {
        public int Id;
        public bool IsInTransfer;
        public Priorities Prioritie;
        public WeightCategories Weight;
        public CustomerInParcel Sender;
        public CustomerInParcel Target;
        public Location PickupLocation;
        public Location TargetLocation;
        public double Distance;
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Is in transer:                 {IsInTransfer}\n" +
                $"Prioritie status:              {Prioritie}\n" +
                $"Weight status:                 {Weight}\n" +
                $"========Receiver=======================\n\t{Target.ToString().Replace("\n", "\n\t")}\n"+
                $"========Sender=========================\n\t{Sender.ToString().Replace("\n", "\n\t")}\n"+                
                $"Pick up location:              {PickupLocation}\n" +
                $"Target location:               {TargetLocation}\n" +
                $"Distance frome target:         {Distance}\n";
                

            return toString;
        }
    }
}
