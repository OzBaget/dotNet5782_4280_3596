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
    }
}
