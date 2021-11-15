using System;
using IBL.BO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL
    {
        public void AddBase(int id, string name, double lat, double lng, int chargSlots)
        {
            try
            {
                DalObject.AddBase(id, name, lat, lng, chargSlots);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException($"ID #{ex.Id} already exists!", ex.Id);
            }
        }

        public void UpdateStation(int id, string name, int numChargers)
        {
            IDAL.DO.Station tmpStation= DalObject.GetBaseStation(id);
            
            throw new NotImplementedException();
        }
    }
}
