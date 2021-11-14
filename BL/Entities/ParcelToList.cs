using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public struct ParcelToList
    {
        public int Id;
        public string Sender;
        public string Receiver;
        public WeightCategories Weight;
        public Priorities Prioritie;
        public ParcelStatus StatusParcel;
    }
}
