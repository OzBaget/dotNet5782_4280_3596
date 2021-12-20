using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public Permissions permission { get; set; }
        public List<ParcelInCustomer> SentParcels { get; set; }
        public List<ParcelInCustomer> ReceivedParcels { get; set; }
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Location:                      {Location}\n" +
                $"Phone number:                  {Phone}" +
                $"Permission:                  {permission}";
            if (SentParcels.Count > 0)
            {
                int count = 1;
                toString +=  $"\n - Parcels which wait to send:";
                foreach (var parcel in SentParcels)
                {
                    toString += $"\n\t===========Parcel #{count}===========\n\t" + parcel.ToString().Replace("\n", "\n\t");
                    parcel.ToString();
                    ++count;
                }
            }
            if (ReceivedParcels.Count > 0)
            {
                int count = 1;
                toString = toString + "\n - Parcels which received:";
                foreach (var parcel in ReceivedParcels)
                {
                    toString += $"\n\t===========Parcel #{count}===========\n\t" + parcel.ToString().Replace("\n", "\n\t");
                    ++count;
                }
            }
            return toString;

        }

    }
}
