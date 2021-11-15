using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int Id;
        public string Name;
        public string Phone;
        public int ParcelsDelivered;
        public int ParcelsSent;
        public int ParcelsReceived;
        public int ParcelsInProccesToHim;

        public override string ToString()
        {
            return 
                $"ID:                        {Id}\n" +
                $"Name:                      {Name}\n" +
                $"Phone:                     {Phone}\n" +
                $"Parcels delivered:         {ParcelsDelivered}\n" +
                $"Parcels sent:              {ParcelsSent}\n" +
                $"Parcels received:          {ParcelsReceived}\n" +
                $"Parcels in the way to him: {ParcelsInProccesToHim}";
        }
    }
}
