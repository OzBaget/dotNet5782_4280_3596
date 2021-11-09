using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id;
        public string Sender;
        public string Receiver;
        public Enums.WeightCategories Weight;
        public Enums.Priorities Prioritie;
        public Enums.ParcelStatus StatusParcel;
        public CustomerInParcel Customer;
    }
}
