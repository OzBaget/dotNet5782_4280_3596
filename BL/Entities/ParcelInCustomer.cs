using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public struct ParcelInCustomer
    {
        public int Id;
        public WeightCategories Weight;
        public Priorities Prioritie;
        public ParcelStatus StatusParcel;
        public CustomerInParcel Customer;
    }
}
