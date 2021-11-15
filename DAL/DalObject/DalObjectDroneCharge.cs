using System;
using System.Collections.Generic;
using IDAL.DO;


namespace DalObject
{
    public partial class DalObject
    {
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return new List<DroneCharge>(DataSource.Charges);
        }
    }
}
