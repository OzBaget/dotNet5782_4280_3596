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
        public Enums.WeightCategories Weight;
        public Enums.Priorities Prioritie;
        public bool IsInTransfer;
        public CustomerInParcel Sender;
        public CustomerInParcel Receiver;
        public Location Collecting;
        public Location Target;
        public int Distance;
    }
}
