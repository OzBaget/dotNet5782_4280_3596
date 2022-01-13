using DO;
using System.Collections.Generic;
using System.Linq;


namespace Dal
{
    internal sealed partial class DalXml : DalApi.IDal
    {
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return loadXmlToList<DroneCharge>().Where(dc => dc.IsActived);
        }
    }
}
