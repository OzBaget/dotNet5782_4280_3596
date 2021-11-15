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
        public void AddStation(BaseStation station)
        {
            try
            {
                DalObject.AddStation(station.Id, station.Name, station.Location.Latitude, station.Location.Longitude, station.NumFreeChargers);
            }
            catch (IDAL.DO.IdAlreadyExistsException ex)
            {
                throw new IBL.BL.IdAlreadyExistsException(ex.Message, ex.Id);
            }
        }

        public void UpdateStation(int stationId, string name, int numChargers)
        {
            
            BaseStation tmpStation= GetStation(stationId);
            DalObject.DeleteStation(stationId);
            DalObject.AddStation(tmpStation.Id, name, tmpStation.Location.Latitude, tmpStation.Location.Longitude, numChargers);
      
            
        }
    }
}
