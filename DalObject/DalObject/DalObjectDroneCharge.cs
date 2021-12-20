using System;
using System.Collections.Generic;
using DO;


namespace Dal
{
    public partial class DalObject : DalApi.IDal
    {
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return new List<DroneCharge>(DataSource.Charges);
        }
    }
}
