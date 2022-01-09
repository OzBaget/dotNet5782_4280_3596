using System.Linq;
using System.Collections.Generic;
using DO;


namespace Dal
{
    sealed partial class DalXml : DalApi.IDal
    {
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return loadXmlToList<DroneCharge>().Where(dc => dc.IsActived);
        }
    }
}
