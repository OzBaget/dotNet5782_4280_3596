using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DO;


namespace Dal
{
    sealed partial class DalObject : DalApi.IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return new List<DroneCharge>(DataSource.Charges);
        }
    }
}
