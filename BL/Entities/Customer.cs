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
                $"ID:                   {Id}\n" +
                $"Name:                 {Name}\n" +
                $"Location:             {Location}\n" +
                $"Phone number:         {Phone}\n" +
                $"Parcels which wait to send:\n";
            foreach (var parcel in WaitsToSendParcels)
            {
                toString += "=============\n" + parcel.ToString();
            }
            toString = toString + "Parcels which received:\n";
            foreach (var parcel in ReceivedParcels)
            {
                toString += "=============\n" + parcel.ToString();
            }
            return toString;

        }

    }
}
