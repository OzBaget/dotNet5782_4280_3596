using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int Id;
        public string Name;
        public string Phone;
        public Location Location;
        public List<ParcelInCustomer> WaitsToSendParcels;
        public List<ParcelInCustomer> ReceivedParcels;
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Location:                      {Location}\n" +
                $"Phone number:                  {Phone}";
            if (WaitsToSendParcels.Count > 0)
            {
                int count = 1;
                toString +=  $"\n - Parcels which wait to send:";
                foreach (var parcel in WaitsToSendParcels)
                {
                    toString += $"\n===========Parcel #{count}===========\n" + parcel.ToString();
                    ++count;
                }
            }
            if (ReceivedParcels.Count > 0)
            {
                int count = 1;
                toString = toString + "\n - Parcels which received:";
                foreach (var parcel in ReceivedParcels)
                {
                    toString += $"\n===========Parcel #{count}===========\n" + parcel.ToString();
                    ++count;
                }
            }
            return toString;

        }

    }
}
