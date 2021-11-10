using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int Id;
        public Enums.WeightCategories Weight;
        public Enums.Priorities Prioritie;
        public Enums.ParcelStatus StatusParcel;
        public CustomerInParcel Customer;
    }
}
