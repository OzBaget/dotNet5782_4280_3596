using System.Collections.Generic;

namespace BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<ParcelInCustomer> SentParcels { get; set; }
        public List<ParcelInCustomer> ReceivedParcels { get; set; }
        public override string ToString()
        {
            string toString =
                $"ID:                            {Id}\n" +
                $"Name:                          {Name}\n" +
                $"Location:                      {Location}\n" +
                $"Phone number:                  {Phone}";
            if (SentParcels.Count > 0)
            {
                int count = 1;
                toString += $"\n - Parcels which wait to send:";
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
