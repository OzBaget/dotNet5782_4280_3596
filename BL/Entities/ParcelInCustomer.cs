using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Prioritie { get; set; }
        public ParcelStatus StatusParcel { get; set; }
        public CustomerInParcel Customer { get; set; }
        public override string ToString()
        {
            string toString =
                 $"ID:                            {Id}\n" +
                 $"Weight status:                 {Weight}\n" +
                 $"Prioritie status:              {Prioritie}\n" +
                 $"Parcel status:                 {StatusParcel}\n"+
                 $"        ============Customer===========\n\t{Customer.ToString().Replace("\n", "\n\t")}";                                
            return toString;
        }
    }
}
