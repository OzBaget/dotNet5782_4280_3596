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

    }
}
