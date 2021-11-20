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
        public WeightCategories Weight;
        public Priorities Prioritie;
        public ParcelStatus StatusParcel;
        public CustomerInParcel Customer;
        public override string ToString()
        {
            string toString =
                            $"ID:                   {Id}\n" +
                            $"Weight status:        {Weight}\n" +
                            $"Prioritie status:     {Prioritie}\n" +
                            $"Parcel status:        {StatusParcel}\n" +
                            $"Customer :            {Customer}\n";
            return toString;
        }
    }
}
